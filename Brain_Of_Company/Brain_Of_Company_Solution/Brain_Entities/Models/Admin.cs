using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain_Entities.Models
{
    public class Admin
    {
        [ForeignKey("SSN")]
        [Key]
        public string SSN { get; set; }
        public string Password { get; set; }
        public Employee Employee { get; set; }
    }
    
}
