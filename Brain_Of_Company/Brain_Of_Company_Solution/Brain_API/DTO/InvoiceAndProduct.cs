using System.ComponentModel.DataAnnotations;

namespace Brain_API.DTO
{
    public class InvoiceAndProduct : InvoiceDTO
    {
        [Required]
        public int Id {  get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public DateTime DateOfSale { get; set; } // Date of Sale (primary key)

    }
}
