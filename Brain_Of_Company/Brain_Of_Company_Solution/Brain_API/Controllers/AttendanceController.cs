using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : APIBaseController
    {
        public AttendanceController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAttendance()
        {
            var attendances  = _unitOfWork.Attendances.GetAll();
            List<ShowShortDataAttendanceDTO> showShortData = attendances.Select(x => new ShowShortDataAttendanceDTO()
            {
                Id = x.Id,
                DateOfDay = x.DateOfDay,
                EmployeeSSN = x.EmployeeSSN,
                IsAttended = x.IsAttended
            }).ToList();
            return Ok(showShortData);
        }


        [HttpGet("GetRateOfAttendance")]
        public async Task<IActionResult> GetRateOfAttendance()
        {
            var attendances = await _unitOfWork.Attendances.GetAllAsync();
            int count = 0;
            if (attendances is null)
                return Ok("there is no attendance yet");
            DateOnly date = DateOnly.FromDateTime(attendances.First().DateOfDay);
            List<RateDTO> rateOfHiringOrFiringDTOs = new List<RateDTO>();
            foreach(var item in attendances)
            {
                if (DateOnly.FromDateTime(item.DateOfDay).CompareTo(date) == 0)
                {
                    if (item.IsAttended == true)
                        count++;
                }
                else
                {
                    rateOfHiringOrFiringDTOs.Add(new RateDTO()
                    {
                        Count = count,
                        Date = date
                    });
                    count = 1;
                    date = DateOnly.FromDateTime(item.DateOfDay);
                }
            }
            rateOfHiringOrFiringDTOs.Add(new RateDTO()
            {
                Count = count,
                Date = date
            });
            return Ok(rateOfHiringOrFiringDTOs);
        }

        [HttpGet("GetTheAbsenceRate")]
        public async Task<IActionResult> GetTheAbsenceRate()
        {
            var attendances = await _unitOfWork.Attendances.GetAllAsync();
            int count = 0;
            if (attendances is null)
                return Ok("there is no attendance yet");
            DateOnly date = DateOnly.FromDateTime(attendances.First().DateOfDay);
            List<RateDTO> rateOfHiringOrFiringDTOs = new List<RateDTO>();
            foreach (var item in attendances)
            {
                if (DateOnly.FromDateTime(item.DateOfDay).CompareTo(date) == 0)
                {
                    if (item.IsAttended == false)
                        count++;
                }
                else
                {
                    rateOfHiringOrFiringDTOs.Add(new RateDTO()
                    {
                        Count = count,
                        Date = date
                    });
                    count = 1;
                    date = DateOnly.FromDateTime(item.DateOfDay);
                }
            }
            rateOfHiringOrFiringDTOs.Add(new RateDTO()
            {
                Count = count,
                Date = date
            });
            return Ok(rateOfHiringOrFiringDTOs);
        }


        [HttpGet("GetAttendanceById")]
        public async Task<IActionResult> GetOneAttendance(int id)
        {
            var attendance = _unitOfWork.Attendances.GetAttendanceWithEmployeesById(id);
            if (attendance is null)
                return NotFound("invalid id");
            ShowAllDataAttendanceDTO showAllData = new ShowAllDataAttendanceDTO()
            {
                Id = attendance.Id,
                DateOfDay = attendance.DateOfDay,
                ShowShortDataAboutEmployeeDTO = new ShowShortDataAboutEmployeeDTO() 
                {
                    SSN = attendance.Employee.SSN,
                    DepartmentId = attendance.Employee.DepartmentId,
                    Email = attendance.Employee.Email,
                    Gender = attendance.Employee.Gender,
                    Name = attendance.Employee.Name,
                    Phone = attendance.Employee.Phone
                }
            };
            return Ok(showAllData);
        }

        [HttpGet("GetAttendanceByDate")]
        public async Task<IActionResult> GetAttendanceByDate(DateTime date)
        {
            var attendances = await _unitOfWork.Attendances.GetAttendanceWithEmployeesByDate(DateOnly.FromDateTime(date));
            if (attendances is null)
                return NotFound("invalid Date");
                List<ShowShortDataAttendanceDTO> showShortData = attendances.Select(x => new ShowShortDataAttendanceDTO()
                {
                    Id = x.Id,
                    DateOfDay = x.DateOfDay,
                    EmployeeSSN = x.EmployeeSSN,
                    IsAttended = x.IsAttended
                }).ToList();
            return Ok(showShortData);
        }
        [HttpGet("GetAttendanceByEmployeeSSN")]
        public async Task<IActionResult> GetAttendanceByEmployeeSSN(string SSN)
        {
            var attendances = await _unitOfWork.Attendances.GetAttendanceByEmployeesSSN(SSN);
            List<HelpShowAttendanceDTO> showShortData = attendances.Select(x => new HelpShowAttendanceDTO()
            {
                Id = x.Id,
                DateOfDay = x.DateOfDay,
                IsAttended = x.IsAttended
            }).ToList();
            return Ok(showShortData);
        }

        [HttpGet("GetAttendanceByEmployeeSSNInSpecificDay")]
        public async Task<IActionResult> GetAttendanceByEmployeeSSNInSpecificDay(string SSN, DateTime Date)
        {
            Attendance? attendance = null;
            DateOnly date = DateOnly.FromDateTime(Date);
            if (SSN.Length == 14 && DateOnly.FromDateTime(DateTime.Now).CompareTo(date) >= 0)
                attendance = _unitOfWork.Attendances.GetAttendanceByEmployeesSSNInSpecificDay(SSN, date);
            if (attendance is null)
                return NotFound("the date or SSN in Valid");
            ShowShortDataAttendanceDTO showShortData = new ShowShortDataAttendanceDTO()
            {
                Id = attendance.Id,
                DateOfDay = attendance.DateOfDay,
                IsAttended = attendance.IsAttended,
                EmployeeSSN = attendance.EmployeeSSN
            };
            return Ok(showShortData);
        }
        [HttpGet("GetAttendanceByEmployeeSSNInRangeOfDays")]
        public async Task<IActionResult> GetAttendanceByEmployeeSSNInRangeOfDays(string SSN, DateTime startDate, DateTime endDate)
        {
            List<Attendance>? attendances = null;
            if (SSN.Length == 14 && DateTime.Now.CompareTo(endDate) >= 0 && startDate.CompareTo(endDate) <= 0)
                attendances = await _unitOfWork.Attendances.GetAttendanceByEmployeesSSNInRange(SSN, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate));
            if (attendances.IsNullOrEmpty())
                return NotFound("the Range of date or SSN is inValid");
            List<HelpShowAttendanceDTO> showShortData = attendances.Select(x => new HelpShowAttendanceDTO()
            {
                Id = x.Id,
                DateOfDay = x.DateOfDay,
                IsAttended = x.IsAttended,
            }).ToList();
            return Ok(showShortData);
        }


        [HttpPost]
        public async Task<IActionResult> AddAttendance(AddAttendanceDTO AddAttendanceDTO)
        {
            if (ModelState.IsValid)
            {
                var checkIsExist = _unitOfWork.Attendances.GetAll().TakeLast(31).Where(x=> DateOnly.FromDateTime(x.DateOfDay).CompareTo(DateOnly.FromDateTime(DateTime.Now)) == 0);
                foreach (var item in checkIsExist)
                {
                    if(AddAttendanceDTO.EmployeeSSN == item.EmployeeSSN)
                        return BadRequest("this employee has register his attendance for today");
                }
                Attendance attendance = new Attendance()
                {
                    DateOfDay = DateTime.Now,
                    EmployeeSSN = AddAttendanceDTO.EmployeeSSN,
                    IsAttended = AddAttendanceDTO.IsAttended,
                };
                _unitOfWork.Attendances.Add(attendance);
                _unitOfWork.Save();
                return Ok("Add attendance success");
            }
            return BadRequest(ModelState.Values.Select(x => x.Errors));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAttendance(UpdateAttendanceDTO updateAttendanceDTO)
        {
            if (ModelState.IsValid)
            {
                Attendance attendance = await _unitOfWork.Attendances.GetByIdAsync(updateAttendanceDTO.Id);
                if(attendance.EmployeeSSN != updateAttendanceDTO.EmployeeSSN)
                {
                    var attendances = _unitOfWork.Attendances.GetAll().Where(x => DateOnly.FromDateTime(x.DateOfDay).CompareTo(DateOnly.FromDateTime(attendance.DateOfDay)) == 0);
                    foreach (var item in attendances)
                        if (item.EmployeeSSN == updateAttendanceDTO.EmployeeSSN)
                            return BadRequest($"this SSN you want to change attendance to it has another attendance his id is {item.Id}");
                }
                attendance.EmployeeSSN= updateAttendanceDTO.EmployeeSSN;
                attendance.IsAttended = updateAttendanceDTO.IsAttended;
                _unitOfWork.Attendances.Update(attendance);
                _unitOfWork.Save();
                return Ok("update attendance success");
            }
            return BadRequest(ModelState.Values.Select(x => x.Errors));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            Attendance? attendance = await _unitOfWork.Attendances.GetByIdAsync(id);
            if (attendance is null)
                return NotFound("invalid id");
            _unitOfWork.Attendances.Delete(attendance);
            _unitOfWork.Save();
            return Ok("delete attendance success");
        }
    
    }
}
