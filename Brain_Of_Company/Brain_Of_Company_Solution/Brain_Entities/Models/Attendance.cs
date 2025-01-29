using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_Entities.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; } 
        public DateTime DateOfDay { get; set; }
        public bool IsAttended { get; set; }
        [ForeignKey("SSN")]
        public string EmployeeSSN { get; set; }
        public Employee Employee { get; set; } // Total from Attendance

    }
}
