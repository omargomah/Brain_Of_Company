using System.ComponentModel.DataAnnotations;

namespace Brain_Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } // Total from Category
    }
}
