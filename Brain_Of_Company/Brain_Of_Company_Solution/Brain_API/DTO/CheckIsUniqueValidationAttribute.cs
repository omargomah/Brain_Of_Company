using Brain_Entities.Models;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    internal class CheckIsUniqueValidationAttribute<T> : ValidationAttribute
        where T : class
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return null;
            IUnitOfWork? unitOfWork = validationContext.GetService<IUnitOfWork>();
            if (unitOfWork is null)
                return new ValidationResult("the Service can't provide");
            if (typeof(T) == typeof(Employee) &&  unitOfWork.Employees.IsEmployeeExistBySSN(value.ToString()))
                return new ValidationResult("the SSN must be unique");
            else if (typeof(T) == typeof(Department) && unitOfWork.Departments.GetByNameAsync(value.ToString()).Result is not null)
                return new ValidationResult("the name of Department must be unique");
            else if (typeof(T) == typeof(Dependent) && unitOfWork.Dependents.GetByNameAsync(value.ToString()).Result is not null)
                return new ValidationResult("the name of dependent must be unique");
            else if (typeof(T) == typeof(Admin) && unitOfWork.Admins.GetAll().FirstOrDefault(x=> value.ToString() == x.SSN ) is not null)
                return new ValidationResult("the SSN of Admin must be unique");
            return ValidationResult.Success;
        }
    }
}
