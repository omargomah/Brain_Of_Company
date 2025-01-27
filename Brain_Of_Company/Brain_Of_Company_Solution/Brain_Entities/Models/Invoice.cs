using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_Entities.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }        
        public DateTime DOS { get; set; } // Date of Sale (primary key)
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantities { get; set; }
        [ForeignKey("Id")]
        public int ProductId { get; set; }        
        public Product Product { get; set; } // Many-to-one with Product
    }
}
