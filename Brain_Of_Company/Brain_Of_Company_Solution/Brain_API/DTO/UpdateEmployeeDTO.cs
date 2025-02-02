﻿using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class UpdateEmployeeDTO: ShareDataBetweenAddAndUpdateDTO
    {
        [Required]
        [CheckSSNISExistValidation]
        [Length(minimumLength:14 ,maximumLength: 14 , ErrorMessage = "the length must be 14")]
        public string SSN { get; set; }
    }
}
