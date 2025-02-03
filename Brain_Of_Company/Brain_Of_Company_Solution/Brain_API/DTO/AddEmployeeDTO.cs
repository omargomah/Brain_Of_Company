using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class AddEmployeeDTO : ShareDataBetweenAddAndUpdateDTO
    {
        [Required]
        //[CheckIsUniqueValidation<Employee>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string SSN { get; set; }
        
    }
    public class AddAdminDTO 
    {
        [Required]
        [CheckSSNISExistValidation<Employee>]
        [CheckIsUniqueValidation<Admin>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string SSN { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
    public class UpdateAdminDTO
    {
        [Required]
        [CheckSSNISExistValidation<Employee>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string SSN { get; set; }
        [Required]
        [MaxLength(50)]
        [CheckPasswordIsValid]
        public string OldPassword { get; set; }
        [Required]
        [MaxLength(50)]

        public string NewPassword { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }



}
