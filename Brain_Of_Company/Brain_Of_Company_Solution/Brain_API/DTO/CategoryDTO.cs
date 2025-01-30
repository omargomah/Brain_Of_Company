using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class CategoryDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
