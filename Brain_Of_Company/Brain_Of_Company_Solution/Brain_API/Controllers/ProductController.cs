using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : APIBaseController
    {
        public ProductController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("GetProductWithCategoryName")]
        public async Task<IActionResult> GetProductWithCategoryName()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var categories = await _unitOfWork.Categories.GetAllAsync(); 

            var listProduct = products.Where(x => x.IsDeleted == false).Select(x => new ProductAndCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == x.CategoryId)?.Name ?? "Unknown",
                RealQuantities = x.RealQuantities,
                SoldQuantity = x.SoldQuantities
            });

            return Ok(listProduct);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct()
        {
            var list = await _unitOfWork.Products.GetAllAsync();
            var listProduct = list.Where(x => x.IsDeleted == false).Select(x => new ProductDTO() { Id = x.Id, Name = x.Name, Price = x.Price , CategoryId = x.CategoryId, RealQuantities = x.RealQuantities, SoldQuantity = x.SoldQuantities });
            return Ok(listProduct);
        }

        [HttpGet("GetTopSelling")]
        public async Task<IActionResult> GetTopSelling()
        {
            var productTop =  _unitOfWork.Products.GetAll().Where(x => x.IsDeleted == false).OrderByDescending(x=>x.SoldQuantities).Take(10);
            return Ok(productTop);
        }

        [HttpGet("AlertLowStock")]
        public async Task<IActionResult> AlertLowStock()
        {
            var listProductAlert = await _unitOfWork.Products.FindAllAsync(x => x.RealQuantities <= 20 && x.IsDeleted == false);
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var productDTO = listProductAlert.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                CategoryId = x.CategoryId,
                RealQuantities = x.RealQuantities,

            });
            return Ok(productDTO);
            
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _unitOfWork.Products.GetByNameAsync(name);
            if (product != null && product.IsDeleted == false)
            {
                ProductDTO productDTO = new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    RealQuantities = product.RealQuantities,
                    SoldQuantity = product.SoldQuantities
                };
                return Ok(productDTO);
            }
            return BadRequest("Not Found");
        }

        [HttpPut("EditProduct")]
        public async Task<IActionResult> EditProduct(ProductDTO ProductDTO)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(ProductDTO.Id);
            if (product != null)
            {
                
                product.Name = ProductDTO.Name;
                var tempo = await _unitOfWork.Products.GetByNameAsync(ProductDTO.Name);
                if ((tempo == null || tempo.Id == product.Id) && tempo.IsDeleted == false)
                {
                    product.Price = ProductDTO.Price;
                    product.RealQuantities = ProductDTO.RealQuantities;
                    product.SoldQuantities = ProductDTO.SoldQuantity;

                    _unitOfWork.Save();
                    return Ok("Updated");
                }
            }
            return BadRequest("Invalid Id");
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTOAdd productDTOAdd)
        {
            var pro = await _unitOfWork.Products.GetByNameAsync(productDTOAdd.Name);
            if (pro == null || pro.IsDeleted == true)
            {
                var Product = new Product()
                {
                    DOA = DateTime.Now,
                    Price = productDTOAdd.Price,
                    CategoryId = productDTOAdd.CategoryId,
                    RealQuantities = productDTOAdd.RealQuantities,
                    SoldQuantities = 0,
                    IsDeleted = false,
                    Name = productDTOAdd.Name,
                };
                await _unitOfWork.Products.AddAsync(Product);
                _unitOfWork.Save();
                return Ok("Added!");
            }
            return BadRequest("Item Already Exist");
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(ProductDtoWithoutId productDTOWithoutId)
        {
            var product = await _unitOfWork.Products.GetByNameAsync(productDTOWithoutId.Name);
            if (product != null)
            {
                product.IsDeleted = true;
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Not Found");
        }

        [HttpDelete("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Not Found");
        }
    }
}
