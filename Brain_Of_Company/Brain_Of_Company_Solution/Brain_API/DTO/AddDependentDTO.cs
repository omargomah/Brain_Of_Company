using Brain_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class AddDependentDTO
    {
        [Required]
        [CheckIsUniqueValidation<Dependent>]
        [MaxLength(100)]
        public string Name { get; set; }
    }

}
