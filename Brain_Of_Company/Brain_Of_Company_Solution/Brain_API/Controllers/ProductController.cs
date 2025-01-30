
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProduct()
        {
            var list = await _unitOfWork.Products.GetAllAsync();
            var listProduct = list.Select(x => new ProductDTO() { Id = x.Id, Name = x.Name, Price = x.Price , CategoryId = x.CategoryId, RealQuantities = x.RealQuantities, SoldQuantity = x.SoldQuantities });
            return Ok(listProduct);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _unitOfWork.Products.GetByNameAsync(name);
            if (product != null)
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
                if (tempo == null || tempo.Id == product.Id)
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
            if (pro == null)
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
