using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFLienContact
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing required primary key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int LienId { get; set; }

        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Role { get; set; }
    }
}