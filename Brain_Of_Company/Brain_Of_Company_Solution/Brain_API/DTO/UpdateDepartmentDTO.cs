﻿using System.ComponentModel.DataAnnotations;
using Brain_Entities.Models;

namespace Brain_API.DTO
{
    public class UpdateDepartmentDTO :AddDepartmentDTO
    {
        [Required]
        [CheckIdExistValidation<Department>]
        public int Id { get; set; }
        [CheckSSNISExistValidation<Employee>]
        public string? ManagerSSN { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        [Range(0, 31)]
        public int MinimumDaysToAttendancePerMonth { get; set; }
    }

}
