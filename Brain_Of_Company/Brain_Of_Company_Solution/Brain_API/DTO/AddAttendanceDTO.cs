using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class AddAttendanceDTO
    {
        [Required]
        public bool IsAttended { get; set; }
        [Required]
        [CheckSSNISExistValidation<Employee>]
        public string EmployeeSSN { get; set; }
    }


    //internal class CheckIsDeletedValidationAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        if (value is null)
    //            return null;
    //        IUnitOfWork unitOfWork = validationContext.GetService<IUnitOfWork>();
    //        if (unitOfWork is null)
    //            return new ValidationResult("can't provide the service");
    //    }
    //}
}
