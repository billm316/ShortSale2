using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFOffer
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing required primary key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Missing required offer date")]
        public DateTime OfferDate { get; set; }

        [Required(ErrorMessage = "Missing required offer amount")]
        public decimal OfferAmt { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Status { get; set; }
    }
}