using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Brain_API.DTO;
using Brain_Entities.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : APIBaseController
    {
        public EmployeeController(IUnitOfWork unitOfWork) : base(unitOfWork) {}
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            List<ShowShortDataAboutEmployeeDTO> showShortData = employees.Where(x=>x.IsDeleted == false).Select(x=> new ShowShortDataAboutEmployeeDTO() 
            {
                Email = x.Email,
                DepartmentId = x.DepartmentId,
                Gender = x.Gender,
                Name = x.Name,
                Phone = x.Phone,
                SSN = x.SSN
            }).ToList();
            return Ok(showShortData);
        }

        [HttpGet("GetRateOfHiring")]
        public async Task<IActionResult> GetRateOfHiring()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            if (employees is null)
                return Ok("there is no employee hiring yet");
            int count = 0;
            DateOnly date = DateOnly.FromDateTime(employees.First().DateOfHiring);
            List<RateDTO> rateOfHiringDTOs = new List<RateDTO>();
            foreach (var item in employees)
            {
                if(DateOnly.FromDateTime(item.DateOfHiring).CompareTo(date) == 0)
                    count++;
                else
                {
                    
                    rateOfHiringDTOs.Add(new RateDTO()
                    {
                        Count = count,
                        Date = date
                    });
                    count = 1;
                    date = DateOnly.FromDateTime(item.DateOfHiring);
                }
            }
            rateOfHiringDTOs.Add(new RateDTO()
            {
                Count = count,
                Date = date
            });
            return Ok(rateOfHiringDTOs);
        }

        [HttpGet("GetRateOfFiring")]
        public async Task<IActionResult> GetRateOfFiring()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            int count = 0;
            employees = employees.Where(x => x.IsDeleted == true);
            if (employees.IsNullOrEmpty())
                return Ok("there is no employee firing yet");
            DateOnly date = DateOnly.FromDateTime((DateTime)employees.First().DateOfFiring);
            List<RateDTO> rateOfHiringOrFiringDTOs = new List<RateDTO>();
            foreach (var item in employees)
            {
                if (DateOnly.FromDateTime((DateTime)item.DateOfFiring).CompareTo(date) == 0)
                    count++;
                else
                {

                    rateOfHiringOrFiringDTOs.Add(new RateDTO()
                    {
                        Count = count,
                        Date = date
                    });
                    count = 1;
                    date = DateOnly.FromDateTime((DateTime)item.DateOfFiring);
                }
            }
            rateOfHiringOrFiringDTOs.Add(new RateDTO()
            {
                Count = count,
                Date = date
            });
            return Ok(rateOfHiringOrFiringDTOs);
        }

        [HttpGet("GetSalaryOfEmployee")]
        public async Task<IActionResult> GetSalaryOfEmployee(string SSN , DateTime startDate)
        {
            DateOnly StartDate = DateOnly.FromDateTime(startDate);
            if (DateOnly.FromDateTime(DateTime.Now).CompareTo(StartDate) < 0)
                return BadRequest("Invalid Date");
            var employee = await _unitOfWork.Employees.GetBySSNAsync(SSN);
            if (employee is null || employee.IsDeleted == true)
                return NotFound("Invalid SSN");
            int count = _unitOfWork.Attendances.GetAll().Where(x=> x.EmployeeSSN == SSN && DateOnly.FromDateTime(x.DateOfDay).CompareTo(StartDate) >= 0 && x.IsAttended == true).Count();
            decimal totalProductSell = _unitOfWork.Invoices.GetAll().Where(x => DateOnly.FromDateTime(x.DOS).CompareTo(StartDate) >= 0).Select(x=>x.Quantities * x.Price).Sum();
            return Ok(count * employee.SalaryPerDay + employee.PercentageOfBonus * totalProductSell);
        }

        [HttpGet("GetTotalSalary")]
        public async Task<IActionResult> GetTotalSalary(DateTime startDate)
        {
            DateOnly StartDate = DateOnly.FromDateTime(startDate);
            if (DateOnly.FromDateTime(DateTime.Now).CompareTo(StartDate) < 0)
                return BadRequest("Invalid Date");
            var employees = await _unitOfWork.Employees.GetAllAsync();
            if (employees is null)
                return Ok("there is no employees yet");
            decimal totalProductSell = _unitOfWork.Invoices.GetAll().Where(x => DateOnly.FromDateTime(x.DOS).CompareTo(StartDate) >= 0).Select(x => x.Quantities * x.Price).Sum();
            decimal bonus , TotalSalary = 0; 
            int count = 0;
            foreach (var item in employees)
            {
                bonus = item.PercentageOfBonus * totalProductSell;
                count = _unitOfWork.Attendances.GetAll().Where(x => x.EmployeeSSN == item.SSN && DateOnly.FromDateTime(x.DateOfDay).CompareTo(StartDate) >= 0 && x.IsAttended  == true).Count();
                TotalSalary += count * item.SalaryPerDay + bonus;
            }
            return Ok(new GetTotalSalaryDTO()
            {
                TotalSalary = TotalSalary,
                CountOfEmployee = employees.Count()
            });

        }

        [HttpGet("GetDependentsEmployeeBySSN")]
        public async Task<IActionResult> GetDependentsEmployeeBySSN(string SSN)
        {
            if (SSN.Length != 14)
                return BadRequest("the length of SSN must be 14");
            var employee = await _unitOfWork.Employees.GetBySSNAsync(SSN);
            if (employee is null || employee.IsDeleted == true)
                return NotFound("invalid SSN");
            var dependents = _unitOfWork.Dependent_Employees.GetAllDependentEmployeeWithDependentBySSN(SSN);
            ShowDependentOfEmployeeDTO showShortData = new ShowDependentOfEmployeeDTO()
            {
                EmployeeName = employee.Name,
                EmployeeSSN = employee.SSN,
                dataDependentDTOs = dependents.Select(x => new ShowShortDataDependentDTO()
                {
                    Id = x.DependentId,
                    Name = x.Dependent.Name
                }).ToList()
            };
            return Ok(showShortData);

        }

        [HttpGet("GetEmployeeBySSN")]
        public async Task<IActionResult> GetOneEmployee(string SSN)
        {
            if (SSN.Length != 14)
                return BadRequest("the length of SSN must be 14");
            var employee = await _unitOfWork.Employees.GetBySSNAsync(SSN);
            if (employee is null || employee.IsDeleted == true)
                return NotFound("invalid SSN");
            ShowAllDataAboutEmployee showShortData = new ShowAllDataAboutEmployee()
            {
                Email = employee.Email,
                DepartmentId = employee.DepartmentId,
                Gender = employee.Gender,
                Name = employee.Name,
                Phone = employee.Phone,
                SSN = employee.SSN,
                City = employee.City,
                Country = employee.Country,
                DateOfBirth = employee.DateOfBirth,
                DateOfHiring = employee.DateOfHiring,
                PercentageOfBonus = employee.PercentageOfBonus,
                SalaryPerDay = employee.SalaryPerDay,
                Street = employee.Street
            };
            return Ok(showShortData);
        
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO addEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                Employee? employeeIfExist = await _unitOfWork.Employees.GetBySSNAsync(addEmployeeDTO.SSN);
                if (employeeIfExist is not null)
                {
                    if (employeeIfExist.IsDeleted == true)
                    {
                        employeeIfExist.IsDeleted = false;
                        _unitOfWork.Employees.Update(employeeIfExist);
                        _unitOfWork.Save();
                        return Ok("the employee is exist before and leave so i return him again you must add the dependents that he had before leave");

                    }
                    else
                        return BadRequest("the SSN must be unique");

                }
                Employee employee = new Employee()
                {
                    Email = addEmployeeDTO.Email,
                    DepartmentId = addEmployeeDTO.DepartmentId,
                    Gender = addEmployeeDTO.Gender,
                    Name = addEmployeeDTO.Name,
                    Phone = addEmployeeDTO.Phone,
                    SSN = addEmployeeDTO.SSN,
                    City = addEmployeeDTO.City,
                    Country = addEmployeeDTO.Country,
                    DateOfBirth = addEmployeeDTO.DateOfBirth,
                    DateOfFiring = null,
                    DateOfHiring = DateTime.Now,
                    PercentageOfBonus = addEmployeeDTO.PercentageOfBonus,
                    SalaryPerDay = addEmployeeDTO.SalaryPerDay,
                    Street = addEmployeeDTO.Street,
                    IsDeleted = false
                };
                _unitOfWork.Employees.Add(employee);
                _unitOfWork.Save();
                return Ok("the Add done");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO UpdateEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                if(UpdateEmployeeDTO.OldSSN.CompareTo(UpdateEmployeeDTO.NewSSN) !=0)
                {
                    Employee employee1 = await _unitOfWork.Employees.GetBySSNAsync(UpdateEmployeeDTO.NewSSN);
                    if (employee1 is not null)
                        return BadRequest("the new SSN you enter used by another employee");
                }
                Employee employee  = await _unitOfWork.Employees.GetBySSNAsync(UpdateEmployeeDTO.OldSSN);
                employee.Email = UpdateEmployeeDTO.Email;
                employee.DepartmentId = UpdateEmployeeDTO.DepartmentId;
                employee.Gender = UpdateEmployeeDTO.Gender;
                employee.Name = UpdateEmployeeDTO.Name;
                employee.Phone = UpdateEmployeeDTO.Phone;
                employee.SSN = UpdateEmployeeDTO.NewSSN;
                employee.City = UpdateEmployeeDTO.City;
                employee.Country = UpdateEmployeeDTO.Country;
                employee.DateOfBirth = UpdateEmployeeDTO.DateOfBirth;
                employee.PercentageOfBonus = UpdateEmployeeDTO.PercentageOfBonus;
                employee.SalaryPerDay = UpdateEmployeeDTO.SalaryPerDay;
                employee.Street = UpdateEmployeeDTO.Street;
                _unitOfWork.Employees.Update(employee);
                _unitOfWork.Save();
                return Ok("the update done");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string SSN)
        {
            if(SSN.Length != 14)
                return BadRequest("the length of SSN must equal 14");
            Employee? employee = await _unitOfWork.Employees.GetBySSNAsync(SSN);
            if(employee is null)
                return NotFound("invalid SSN");
            Department? department = _unitOfWork.Departments.GetAll().FirstOrDefault(x=>x.ManagerSSN == employee.SSN);
            if(department is not null)
            {
                department.ManagerSSN = null;
                _unitOfWork.Departments.Update(department);
            }
            Admin admin = await _unitOfWork.Admins.GetBySSNAsync(SSN);
            if(admin is not null)
            {
                _unitOfWork.Admins.Delete(admin);
                _unitOfWork.Save();
            }
            var dependent_Employees = _unitOfWork.Dependent_Employees.GetAll().Where(x=>x.EmployeeSSN == employee.SSN);
            _unitOfWork.Dependent_Employees.DeleteRange(dependent_Employees);
            _unitOfWork.Save();
            employee.IsDeleted = true;
            employee.DateOfFiring = DateTime.Now;
            _unitOfWork.Employees.Update(employee);
            _unitOfWork.Save();
            return Ok("Delete done");
        }
    }
}
