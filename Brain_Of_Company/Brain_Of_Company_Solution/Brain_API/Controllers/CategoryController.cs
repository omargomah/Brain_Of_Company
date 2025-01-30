using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
