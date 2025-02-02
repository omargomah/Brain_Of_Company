namespace Brain_API.DTO
{
    public class HelpShowAttendanceDTO
    {
        public int Id { get; set; }
        public DateTime DateOfDay { get; set; }
        public bool IsAttended { get; set; }
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
