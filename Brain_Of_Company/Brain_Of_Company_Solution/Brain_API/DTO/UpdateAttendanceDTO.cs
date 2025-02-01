using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class UpdateAttendanceDTO : AddAttendanceDTO
    {
        [Required]
        [Range(0 ,maximum:int.MaxValue)]
        [CheckIdExistValidation<Attendance>]
        public int Id { get; set; }
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
