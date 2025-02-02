using System.Text.Json.Serialization;

namespace Brain_API.DTO
{
    public class GetTotalSalaryDTO
    {
        [JsonPropertyName("Count Of Employee")]
        public int CountOfEmployee { get; set; }
        [JsonPropertyName("Total Salary ")]
        public decimal TotalSalary { get; set; }
    }
}
