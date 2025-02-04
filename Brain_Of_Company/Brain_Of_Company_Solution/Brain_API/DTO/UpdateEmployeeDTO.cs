using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class UpdateEmployeeDTO: ShareDataBetweenAddAndUpdateDTO
    {
        [Required]
        [CheckSSNISExistValidation<Employee>]
        [Length(minimumLength:14 ,maximumLength: 14 , ErrorMessage = "the length must be 14")]
        public string OldSSN { get; set; }
        [Required]
        [Length(minimumLength:14 ,maximumLength: 14 , ErrorMessage = "the length must be 14")]
        public string NewSSN { get; set; }
    }
}
