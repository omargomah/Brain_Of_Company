using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brain_Entities.Models
{
    public class Dependent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property to link Dependent to Employee (weak entity)
        public List<Dependent_Employee>  dependent_Employees{ get; set; } = new List<Dependent_Employee>();


    }
}
