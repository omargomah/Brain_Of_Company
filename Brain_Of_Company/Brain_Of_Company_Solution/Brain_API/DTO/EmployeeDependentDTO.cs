using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class EmployeeDependentDTO
    {
        [Required]
        public int DependentId { get; set; }
        [Required]
        public string EmployeeSSN { get; set; }
    }
    

}
