using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Brain_Entities.Models
{
    [PrimaryKey("DependentId", "EmployeeSSN")]
    public class Dependent_Employee
    {
        [Column(Order = 0)]
        [ForeignKey("Id")]
        public int DependentId { get; set; }
        [Column(Order = 1)]
        [ForeignKey("SSN")]
        public string EmployeeSSN { get; set; }
        public Employee Employee { get; set; }
        public Dependent Dependent { get; set; }
    }
}
