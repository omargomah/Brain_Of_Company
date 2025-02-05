using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Brain_API.Controllers;
using DAL;
using Brain_DAL.Migrations;
using Brain_API.DTO;
using Brain_Entities.Models;
using Brain_API.Services;
using BCrypt.Net;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AdminController : APIBaseController
    {
        private JwtService jwt;
        public AdminController(IUnitOfWork unitOfWork,JwtService jwt) : base(unitOfWork) 
        {
            this.jwt = jwt;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAdmin(AddAdminDTO addAdminDTO)
        {
            if(ModelState.IsValid)
            {
                Admin admin = new Admin() 
                {
                    SSN = addAdminDTO.SSN,
                    Password = BCrypt.Net.BCrypt.HashPassword(addAdminDTO.Password)
                };
                await _unitOfWork.Admins.AddAsync(admin);
                _unitOfWork.Save();
                return Ok("Add admin success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpDelete]
        [Authorize]
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
        [Authorize]
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
                if(!BCrypt.Net.BCrypt.Verify(updateAdminDTO.OldPassword, admin.Password))
                    return BadRequest("the old Password incorrect");
                admin.SSN = updateAdminDTO.NewSSN;
                admin.Password = BCrypt.Net.BCrypt.HashPassword(updateAdminDTO.NewPassword);
                _unitOfWork.Admins.Update(admin);
                _unitOfWork.Save();
                return Ok("update admin success");
            }
            return BadRequest(ModelState.Values.Select(x=>x.Errors));
        }
        [HttpGet("GetAll")]
        [Authorize]
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
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAndTokenAsync(LoginDTO loginDto)
        {
            Admin admin = await _unitOfWork.Admins.FindAsync(u => u.SSN == loginDto.SSN);
            if (admin == null)
                return BadRequest("Username or Password is incorrect");
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.Password);

            if (!isPasswordValid)
                return BadRequest("Username or Password is incorrect");
            return Ok(new
            {
                token = jwt.GenerateJSONWebToken(admin),
                expiration = DateTime.UtcNow.AddHours(8)
            });
        }
    }
}