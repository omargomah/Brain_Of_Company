using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class UpdateAdminDTO
    {
        [Required]
        [CheckSSNISExistValidation<Admin>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string OldSSN { get; set; }
        [Required]
        [CheckSSNISExistValidation<Employee>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string NewSSN { get; set; }
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
