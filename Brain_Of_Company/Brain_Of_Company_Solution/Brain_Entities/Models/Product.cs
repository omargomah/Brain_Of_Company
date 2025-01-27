using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_Entities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DOA { get; set; } // Date of Arrival
        public DateTime? DOD { get; set; } // Date of Departure (nullable)
        public bool IsDeleted { get; set; }
        public int SoldQuantities { get; set; }
        public int RealQuantities { get; set; }
        [ForeignKey("Id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // One-to-many with Category
        public List<Invoice> Invoices { get; set; } // One-to-many with Invoices
    }
}
