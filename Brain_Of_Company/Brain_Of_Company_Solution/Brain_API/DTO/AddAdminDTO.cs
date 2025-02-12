﻿using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class AddAdminDTO 
    {
        [Required]
        [CheckSSNISExistValidation<Employee>]
        [CheckIsUniqueValidation<Admin>]
        [Length(minimumLength: 14, maximumLength: 14, ErrorMessage = "the length must be 14")]
        public string SSN { get; set; }
        [Required]
        [Length(maximumLength:50,minimumLength:8)]
        public string Password { get; set; }
        [Required]
        [Length(maximumLength:50,minimumLength:8)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }



}
