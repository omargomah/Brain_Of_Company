using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class ProductDTO : ProductDTOAdd
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SoldQuantity { get; set; }
    }
}
