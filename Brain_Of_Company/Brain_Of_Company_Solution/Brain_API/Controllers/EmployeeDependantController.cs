using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeDependantController : APIBaseController
    {
        public EmployeeDependantController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDependents()
        {
            var list = await _unitOfWork.Dependent_Employees.GetAllAsync();
            var dependents = list.Select(x => new EmployeeDependentDTO()
            {
                DependentId = x.DependentId,
                EmployeeSSN = x.EmployeeSSN
            });
            return Ok(dependents);
        }

        [HttpGet("GetByEmployeeSSN")]
        public async Task<IActionResult> GetByEmployeeSSN(string employeeSSN)
        {
            var dependents = await _unitOfWork.Dependent_Employees.FindAllAsync(x => x.EmployeeSSN == employeeSSN);

            if (dependents == null || !dependents.Any())
            {
                return NotFound("No dependents found for this employee.");
            }

            var dependentDTOs = dependents.Select(x => new EmployeeDependentDTO()
            {
                DependentId = x.DependentId,
                EmployeeSSN = x.EmployeeSSN
            });

            return Ok(dependentDTOs);
        }

        [HttpPut("EditDependent")]
        public async Task<IActionResult> EditDependent(EmployeeDependentDTO dependentDTO)
        {
            var dependent = _unitOfWork.Dependent_Employees.GetAll().FirstOrDefault(x => x.EmployeeSSN == dependentDTO.EmployeeSSN);
            if (dependent is not null)
            {
                dependent.DependentId = dependentDTO.DependentId;
              int check = _unitOfWork.Save();
                if (check == 0)
                {
                    return Ok("Updated");
                }
                return BadRequest("Invalid SSN or DependentId");
            }
            return BadRequest("Invalid Dependent Id or Employee SSN");
        }

        [HttpPost("AddDependent")]
        public async Task<IActionResult> AddDependent(EmployeeDependentDTO dependentDTO)
        {

            Dependent_Employee? dependent_EmployeeToCheckExist = _unitOfWork.Dependent_Employees.GetAll().FirstOrDefault(x => x.DependentId == dependentDTO.DependentId && x.EmployeeSSN == dependentDTO.EmployeeSSN);
            if (dependent_EmployeeToCheckExist is not null)
                return BadRequest("this employee has this dependent already");
            Dependent_Employee dependent_Employee = new Dependent_Employee()
            {
                DependentId = dependentDTO.DependentId,
                EmployeeSSN = dependentDTO.EmployeeSSN
            };
            await _unitOfWork.Dependent_Employees.AddAsync(dependent_Employee);
            int success = _unitOfWork.Save();
            if (success != 0)
            {
                return Ok("Added!");
            }
            return BadRequest("Could not add dependent.");
        }

        [HttpDelete("DeleteDependent")]
        public async Task<IActionResult> DeleteDependent(int dependentId, string employeeSSN)
        {
            var dependent = await _unitOfWork.Dependent_Employees.FindAsync(x => x.DependentId == dependentId && x.EmployeeSSN == employeeSSN);
            if (dependent is not null)
            {
                _unitOfWork.Dependent_Employees.Delete(dependent);
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Dependent not found.");
        }
    }
}
