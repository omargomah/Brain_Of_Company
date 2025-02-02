namespace Brain_API.DTO
{
    public class ShowAllDataAttendanceDTO : HelpShowAttendanceDTO
    {
        public ShowShortDataAboutEmployeeDTO ShowShortDataAboutEmployeeDTO { get; set; }
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
