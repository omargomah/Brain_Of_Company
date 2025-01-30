using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : APIBaseController
    {
        public EmployeeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllClinics()
        {
            return Ok(await _unitOfWork.Employees.GetAllAsync());
        }
    }
}
