using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class CategoryDTOWithoutId
    {
        [Required]
        public string Name {  get; set; }
    }
}
