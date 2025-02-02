using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_Entities.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
       // [ForeignKey("SSN")]
        public string? ManagerSSN { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MinimumDaysToAttendancePerMonth { get; set; }
        public List<Employee> WorkingEmployees { get; set; } // Partial from Department (Work)
        public Employee? ManagedBy { get; set; } // Total from Department (Manage)
    }
}
