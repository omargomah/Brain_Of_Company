using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class ProductDTOAdd
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int RealQuantities { get; set; }
    }
}
