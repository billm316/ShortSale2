using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFDocHist
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing Primary Key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int EFDocDescId { get; set; }

        public int EFContactId { get; set; }

        public int EFLienId { get; set; }

        public DateTime Date { get; set; }

        public string Action { get; set; }
    }
}