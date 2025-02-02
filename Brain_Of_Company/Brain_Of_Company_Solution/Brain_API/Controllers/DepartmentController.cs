using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : APIBaseController
    {
        public DepartmentController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        [HttpGet("GetAllDEP")]
        public async Task<IActionResult> GetAllDepartments()
        {
             var departments = await _unitOfWork.Departments.GetAllAsync();
            List<ShowShortDataAboutDepartmentDTO>  showShortData = departments.Select(x => new ShowShortDataAboutDepartmentDTO() 
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location,
                ManagerSSN = x.ManagerSSN,
                MinimumDaysToAttendancePerMonth = x.MinimumDaysToAttendancePerMonth
            }).ToList();
            return Ok(showShortData);
        }
        [HttpGet("GetOneDEP")]
        public async Task<IActionResult> GetOneDepartment(int id)
        {
            //Department? department =  _unitOfWork.Departments.GetDepartmentWithEmployeeByDEPIdAsync(id);
            var department = _unitOfWork.Departments.GetById(id);
            if (department is null)
                return NotFound("Invalid Id");
            var employees = _unitOfWork.Employees.GetAll().Where(x=>x.IsDeleted == false && x.DepartmentId == id);
            Employee? manger = null;
            if (department.ManagerSSN is not null)
                manger = _unitOfWork.Employees.GetAll().FirstOrDefault(x => x.SSN == department.ManagerSSN);
            ShowAllDataAboutDepartmentDTO showAllData = new ShowAllDataAboutDepartmentDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location,
                ManagerSSN = department.ManagerSSN,
                MinimumDaysToAttendancePerMonth = department.MinimumDaysToAttendancePerMonth,
                WorkingEmployees = employees.Where(x => x.IsDeleted == false).Select(x => new ShowShortDataAboutEmployeeDTO()
                {
                    Email = x.Email,
                    DepartmentId = x.DepartmentId,
                    Gender = x.Gender,
                    Name = x.Name,
                    Phone = x.Phone,
                    SSN = x.SSN,

                }).ToList()
            };
            if (department.ManagedBy is null)
                department.ManagedBy = null;
            else
            {
                showAllData.ManagedBy = new ShowShortDataAboutEmployeeDTO()
                {
                    Email = manger.Email,
                    DepartmentId = manger.DepartmentId,
                    Gender = manger.Gender,
                    Name = manger.Name,
                    Phone = manger.Phone,
                    SSN = manger.SSN
                };
            }
            return Ok(showAllData);
        }
        [HttpGet("GetDEPManager")]
        public async Task<IActionResult> GetDEPManager(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department is null)
                return NotFound("Invalid Id");
            if(department.ManagerSSN is null)
                return Ok("this department doesn't has manager yet");
            var manger = _unitOfWork.Employees.GetAll().FirstOrDefault(x => x.SSN == department.ManagerSSN);
            
            ShowManagerOfDepartmentDTO showAllData = new ShowManagerOfDepartmentDTO()
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location,
                ManagerSSN = department.ManagerSSN,
                MinimumDaysToAttendancePerMonth = department.MinimumDaysToAttendancePerMonth,
                ManagedBy = new ShowShortDataAboutEmployeeDTO()
                {
                    Email = department.ManagedBy.Email,
                    DepartmentId = department.ManagedBy.DepartmentId,
                    Gender = department.ManagedBy.Gender,
                    Name = department.ManagedBy.Name,
                    Phone = department.ManagedBy.Phone,
                    SSN = department.ManagedBy.SSN
                }
            };
            return Ok(showAllData);
        }
        [HttpPost]
        public Task<IActionResult> AddDepartment(AddDepartmentDTO addDepartment)
        {
            if(ModelState.IsValid)
            {
                Department department = new Department() 
                {
                    Name = addDepartment.Name,
                    Location = addDepartment.Location,
                    ManagerSSN  = addDepartment.ManagerSSN,
                    MinimumDaysToAttendancePerMonth = addDepartment.MinimumDaysToAttendancePerMonth
                };
                _unitOfWork.Departments.Add(department);
                _unitOfWork.Save();
                return Task.FromResult<IActionResult>(Ok("add Department success"));
            }
            return Task.FromResult<IActionResult>(BadRequest(ModelState.Values.Select(x=>x.Errors)));
        }
    
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDTO UpdateDepartment)
        {
            if(ModelState.IsValid)
            {
                Department departmentToUpdate = await _unitOfWork.Departments.GetByIdAsync(UpdateDepartment.Id);
                Department? department;
                if (UpdateDepartment.Name != departmentToUpdate.Name)
                {
                    department = await _unitOfWork.Departments.GetByNameAsync(UpdateDepartment.Name);
                    if (department is not null)
                        return BadRequest("the name of department must be unique");
                }
                if(UpdateDepartment.ManagerSSN is not null && !UpdateDepartment.ManagerSSN.Equals(departmentToUpdate.ManagerSSN))
                {
                    department = _unitOfWork.Departments.GetAll().FirstOrDefault(x => x.ManagerSSN == UpdateDepartment.ManagerSSN);
                    if (department is not null)
                        return BadRequest("the Manager SSN of department must be unique");
                }
                departmentToUpdate.Name = UpdateDepartment.Name;
                departmentToUpdate.Location = UpdateDepartment.Location;
                departmentToUpdate.ManagerSSN  = UpdateDepartment.ManagerSSN;
                departmentToUpdate.MinimumDaysToAttendancePerMonth = UpdateDepartment.MinimumDaysToAttendancePerMonth;
                _unitOfWork.Departments.Update(departmentToUpdate);
                _unitOfWork.Save();
                return Ok("update Department success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department department= await _unitOfWork.Departments.GetByIdAsync(id);
            if (department is null)
                return NotFound("invalid id");
            _unitOfWork.Departments.Delete(department);
            _unitOfWork.Save();
            return Ok("delete Department success");
        }

    }
}
