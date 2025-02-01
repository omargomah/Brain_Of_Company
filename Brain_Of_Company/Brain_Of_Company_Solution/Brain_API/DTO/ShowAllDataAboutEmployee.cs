using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brain_API.DTO
{
    public class ShowAllDataAboutEmployee : ShowShortDataAboutEmployeeDTO 
    {
        [JsonPropertyName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; } // Date of Birth
        [JsonPropertyName("Date Of Hiring")]
        public DateTime DateOfHiring { get; set; } // Date of Hire
        [JsonPropertyName("Salary Per Day")]
        public decimal SalaryPerDay { get; set; }
        [JsonPropertyName("Percentage Of Bonus")]
        public decimal PercentageOfBonus { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }

}
