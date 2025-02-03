using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class UpdateDependentDTO:AddDependentDTO
    {
        [Required]
        [Range(minimum:0 ,maximum: int.MaxValue ,ErrorMessage = "the id must start from zero and larger")]
        [CheckIdExistValidation<Dependent>]
        public int id { get; set; }
    }



}
