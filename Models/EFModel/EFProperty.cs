using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{    
    public class EFProperty
    {
        public EFProperty ()
        {
            isDeleted = false;
            Type = "Res";
        }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing Primary Key")]
        public int Id { get; set; }

        public bool isDeleted { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string Zip { get; set; }
    }
}

