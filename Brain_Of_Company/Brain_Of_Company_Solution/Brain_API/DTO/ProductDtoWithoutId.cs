using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class ProductDtoWithoutId
    {
        [Required]
        public string Name { get; set; }
    }
}
