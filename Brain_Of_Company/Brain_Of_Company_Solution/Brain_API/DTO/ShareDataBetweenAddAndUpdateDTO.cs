using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brain_API.DTO
{
    public class ShareDataBetweenAddAndUpdateDTO
    {
        [Required]
        [Length(minimumLength: 3, maximumLength: 100, ErrorMessage = "the length of name must be in range of 3 to 100")]
        public string Name { get; set; }
        [Required]
        [Length(minimumLength: 3, maximumLength: 100, ErrorMessage = "the length of phone is 11")]
        [RegularExpression("^(011|012|015|010)\\d{8}$")]
        public string Phone { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,10}$", ErrorMessage = "The email must be like this: name@domain.com")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(Female|Male)", ErrorMessage = " you must choose Female or Male")]
        public string Gender { get; set; }
        [Required]
        [CheckIdExistValidation<Department>]
        [JsonPropertyName("Department Id")]
        public int DepartmentId { get; set; }
        [Required]
        [JsonPropertyName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; } // Date of Birth
        [Required]
        [JsonPropertyName("Salary Per Day")]
        public decimal SalaryPerDay { get; set; }
        [Required]
        [JsonPropertyName("Percentage Of Bonus")]
        [Range(minimum: 0, maximum: 100)]
        public decimal PercentageOfBonus { get; set; }
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string Street { get; set; }
    }

}
