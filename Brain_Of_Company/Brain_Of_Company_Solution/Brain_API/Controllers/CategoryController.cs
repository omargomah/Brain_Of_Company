using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : APIBaseController
    {
        public CategoryController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategory() 
        {
            var list = await _unitOfWork.Categories.GetAllAsync();
            var listCategory = list.Select(x => new CategoryDTO() { Id = x.Id, Name = x.Name });
            return Ok(listCategory);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _unitOfWork.Categories.GetByNameAsync(name);
            if (category != null)
            {
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    Name = name,
                    Id = category.Id
                };
                return Ok(categoryDTO);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("GetProductByCategory")]
        public async Task<IActionResult> GetProductsOFTheCategory(int id)
        {
            var cat = await _unitOfWork.Categories.GetByIdAsync(id);
            if (cat != null)
            {
                var listpro = await _unitOfWork.Products.FindAllAsync(x => x.CategoryId == cat.Id && x.IsDeleted == false);
                if (listpro != null)
                {
                    var listProduct = listpro.Select(x => new ProductAndCategory()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        CategoryId = x.CategoryId,
                        CategoryName = cat.Name,
                        RealQuantities = x.RealQuantities,
                        SoldQuantity = x.SoldQuantities
                    });
                    return(Ok(listProduct));
                }
                return(BadRequest("there is no products in this category"));

            }
            return (BadRequest("Invalid Id"));

        }

        [HttpGet("GetProductsCountInCategory")]
        public async Task<IActionResult> GetProductsCount(int id)
        {
            var products = await _unitOfWork.Products.FindAllAsync(x => x.CategoryId == id && x.IsDeleted == false );
            return Ok($"There are {products.Count()} in this category");
        }


        [HttpPut("EditCategory")]
        public async Task<IActionResult> EditCategory(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryDTO.Id);
            if (category != null)
            {
                category.Id = category.Id;
                category.Name = categoryDTO.Name;
               
                _unitOfWork.Save();
                return Ok("Updated");
            }
            return BadRequest("Invalid Id");
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTOWithoutId categoryDTOWithoutId)
        {
            var Cat = await _unitOfWork.Categories.GetByNameAsync(categoryDTOWithoutId.Name);
            if (Cat == null)
            {
                var category = new Category()
                {
                    Name = categoryDTOWithoutId.Name,
                };
                await _unitOfWork.Categories.AddAsync(category);
                _unitOfWork.Save();
                return Ok("Added!");
            }
            return BadRequest("Category already Exist");
            
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(CategoryDTOWithoutId categoryDTOWithoutId)
        {
            var Category = await _unitOfWork.Categories.GetByNameAsync(categoryDTOWithoutId.Name);
            if (Category != null)
            {
                _unitOfWork.Categories.Delete(Category);
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Not Found");
        }

        [HttpDelete("DeleteCategoryById")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var Category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (Category != null)
            {
                _unitOfWork.Categories.Delete(Category);
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Not Found");
        }


    }
}
