using Brain_Entities.Models;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    internal class CheckIdExistValidationAttribute<T> : ValidationAttribute
        where T : class
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return null;
            IUnitOfWork? unitOfWork = validationContext.GetService<IUnitOfWork>();
            bool check = false;
            if (unitOfWork is null)
                return new ValidationResult("the Service can't provide");
            else if (typeof(T) == typeof(Product))
                check =  unitOfWork.Products.GetById((int)value) is not null;
            else if (typeof(T) == typeof(Department))
                check =  unitOfWork.Departments.GetById((int)value) is not null;
            else if (typeof(T) == typeof(Category))
                check =  unitOfWork.Categories.GetById((int)value) is not null;
            else if (typeof(T) == typeof(Invoice))
                check =  unitOfWork.Invoices.GetById((int)value) is not null;
            else if (typeof(T) == typeof(Attendance))
                check =  unitOfWork.Attendances.GetById((int)value) is not null;
            else if (typeof(T) == typeof(Dependent))
                check =  unitOfWork.Dependents.GetById((int)value) is not null;
            if (check == true)
                return ValidationResult.Success;
            return new ValidationResult($"the id of {typeof(T).Name} is invalid");
        }
    }
}
