using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFDoc
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing Primary Key")]
        public int Id { get; set; }
    }
}