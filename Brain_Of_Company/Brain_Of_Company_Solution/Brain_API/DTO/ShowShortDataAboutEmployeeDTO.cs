using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Brain_API.DTO
{
    public class ShowShortDataAboutEmployeeDTO
    {
        public string SSN { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        [JsonPropertyName("Department Id")]
        public int DepartmentId { get; set; }
    }
}
