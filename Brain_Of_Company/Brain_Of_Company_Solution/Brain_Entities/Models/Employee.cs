using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_Entities.Models
{
    public class Employee
    {
        [Key]
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; } // Date of Birth
        public DateTime DateOfHiring { get; set; } // Date of Hire
        public decimal SalaryPerDay { get; set; }
        public bool IsDeleted { get; set; }
        public string Phone { get; set; }
        public decimal PercentageOfBonus { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        // Assuming Gender is stored as a string (e.g., "Female", "Male")

        // Navigation property for Dependents (weak entity)
        [ForeignKey("Id")]
        public int DepartmentId { get; set; }
        public Department WorkingDepartment { get; set; } // Many-to-one with Department (Work)
        public Department ManagedDepartment { get; set; } // One-to-one with Department (Manage)
        public List<Attendance> Attendances { get; set; } // Many-to-one with Attendance
        public List<Dependent> Dependents { get; set; } // Many-to-many with Dependents

    }
}
