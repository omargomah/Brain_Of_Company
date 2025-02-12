﻿using System.ComponentModel.DataAnnotations;
using Brain_Entities.Models;

namespace Brain_API.DTO
{
    public class AddDepartmentDTO
    {
        [CheckSSNISExistValidation<Employee>]
        public string? ManagerSSN { get; set; }
        [Required]
        [CheckIsUniqueValidation<Department>]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        [Range(0 ,31)]
        public int MinimumDaysToAttendancePerMonth { get; set; }
    }

}
