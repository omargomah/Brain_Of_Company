using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class EmployeeDependentDTO
    {
        [Required]
        [CheckIdExistValidation<Dependent>]
        public int DependentId { get; set; }
        [Required]
        [CheckSSNISExistValidation<Employee>]
        public string EmployeeSSN { get; set; }
    }
   
    

}
