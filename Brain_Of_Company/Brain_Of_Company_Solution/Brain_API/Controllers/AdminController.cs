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
        public AdminController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
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
        //[HttpPut]
        //public async Task<IActionResult> UpdateAdmin(UpdateAdminDTO updateAdminDTO)
        //{
        //    //var admin = _unitOfWork.Admins.GetAll().FirstOrDefault(x => x.SSN == SSN);
        //    if (admin is null)
        //        return NotFound("inValid SSN");
        //    _unitOfWork.Admins.Delete(admin);
        //    _unitOfWork.Save();
        //    return Ok("delete admin success");
        //}





    }
}