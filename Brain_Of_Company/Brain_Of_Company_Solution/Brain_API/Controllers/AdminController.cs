using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Brain_API.Controllers;
using DAL;
using Brain_DAL.Migrations;
using Brain_API.DTO;
using Brain_Entities.Models;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : APIBaseController
    {
        public AdminController(IUnitOfWork unitOfWork) : base(unitOfWork) {}
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AddAdminDTO addAdminDTO)
        {
            if(ModelState.IsValid)
            {
                Admin admin = new Admin() 
                {
                    SSN = addAdminDTO.SSN,
                    Password = addAdminDTO.Password
                };
                await _unitOfWork.Admins.AddAsync(admin);
                _unitOfWork.Save();
                return Ok("Add admin success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(string SSN)
        {
            var admin = _unitOfWork.Admins.GetAll().FirstOrDefault(x => x.SSN == SSN);
            if (admin is null)
                return NotFound("inValid SSN");
            _unitOfWork.Admins.Delete(admin);
            _unitOfWork.Save();
            return Ok("delete admin success");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAdmin(UpdateAdminDTO updateAdminDTO)
        {
            if (ModelState.IsValid)
            {
                if(updateAdminDTO.OldSSN.CompareTo(updateAdminDTO.NewSSN) !=0)
                {
                    Admin admin1 = await _unitOfWork.Admins.GetBySSNAsync(updateAdminDTO.NewSSN);
                    if (admin1 is not null)
                        return BadRequest("the new SSN is exist already in admin");
                }
                var admin = _unitOfWork.Admins.GetAll().FirstOrDefault(x => x.SSN == updateAdminDTO.OldSSN);
                if (updateAdminDTO.OldPassword.CompareTo(admin.Password) != 0)
                    return BadRequest("the old Password incorrect");
                admin.SSN = updateAdminDTO.NewSSN;
                admin.Password = updateAdminDTO.NewPassword;
                _unitOfWork.Admins.Update(admin);
                _unitOfWork.Save();
                return Ok("update admin success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var admins = await _unitOfWork.Admins.GetAllAdminWithEmployeeAsync();
            List<ShowShortDataAboutEmployeeDTO> showShortData = admins.Select(x=>new ShowShortDataAboutEmployeeDTO() 
            {
                SSN = x.SSN,
                DepartmentId = x.Employee.DepartmentId,
                Email = x.Employee.Email,
                Gender = x.Employee.Gender,
                Name = x.Employee.Name,
                Phone = x.Employee.Phone
            }).ToList();
            return Ok(showShortData);
        }
    }
}