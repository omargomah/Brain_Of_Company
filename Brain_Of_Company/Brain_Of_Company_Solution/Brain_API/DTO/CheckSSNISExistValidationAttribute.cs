using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    internal class CheckSSNISExistValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;
            IUnitOfWork? unitOfWork = validationContext.GetService<IUnitOfWork>();
            if (unitOfWork is null)
                return new ValidationResult("the Service can't provide");
            if (unitOfWork.Employees.IsEmployeeExistBySSN(value.ToString()))
                return ValidationResult.Success;
            return new ValidationResult("the SSN Invalid");

        }
    }
}
