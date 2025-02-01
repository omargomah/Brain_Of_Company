using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class ProductAndCategory : ProductDTO
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
