using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    
    public class InvoiceDTO
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantities { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
