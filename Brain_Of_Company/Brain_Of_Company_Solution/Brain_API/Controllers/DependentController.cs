using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependentController : APIBaseController
    {
        public DependentController(IUnitOfWork unitOfWork): base(unitOfWork) { }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDependent() 
        {
           var dependet = _unitOfWork.Dependents.GetAll();
            List<ShowShortDataDependentDTO >showShortData = dependet.Select(x => new ShowShortDataDependentDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Ok(showShortData);
        }
    
        [HttpGet("GetOne")]
        public async Task<IActionResult> GetOneDependent(int id) 
        {
           var dependent = _unitOfWork.Dependents.GetDependentWithEmployeesById(id);
            if (dependent is null)
                return NotFound("invalid id");
            ShowAllDataDependentDTO showAllData = new ShowAllDataDependentDTO()
            {
                Id = dependent.Id,
                Name = dependent.Name,
                Employee = dependent.dependent_Employees.Where(x=>x.Employee.IsDeleted == false).Select(x=>new ShowShortDataAboutEmployeeDTO() 
                {
                    SSN = x.Employee.SSN,
                    DepartmentId = x.Employee.DepartmentId,
                    Email = x.Employee.Email,
                    Gender = x.Employee.Gender,
                    Name = x.Employee.Name,
                    Phone = x.Employee.Phone
                }).ToList()
            };
            return Ok(showAllData);
        }
        [HttpPost]
        public async Task<IActionResult> AddDependent(AddDependentDTO addDependentDTO) 
        {
            if(ModelState.IsValid)
            {
                Dependent dependent = new Dependent() 
                {
                    Name = addDependentDTO.Name
                };
                _unitOfWork.Dependents.Add(dependent);
                _unitOfWork.Save();
                return Ok("Add dependent success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDependent(UpdateDependentDTO updateDependentDTO)
        {
            if (ModelState.IsValid)
            {
                Dependent dependent = await _unitOfWork.Dependents.GetByIdAsync(updateDependentDTO.id);
                dependent.Name = updateDependentDTO.Name;
                _unitOfWork.Dependents.Update(dependent);
                _unitOfWork.Save();
                return Ok("update dependent success");
            }
            return BadRequest(ModelState.Values.Select(x => x.Errors));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDependent(int id)
        {
                Dependent? dependent = await _unitOfWork.Dependents.GetByIdAsync(id);
            if (dependent is null)
                return NotFound("invalid id");
            _unitOfWork.Dependents.Delete(dependent);
            _unitOfWork.Save();
            return Ok("delete dependent success");

        }
    }
}
