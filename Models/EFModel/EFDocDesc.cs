using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFDocDesc
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing Primary Key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int PropertyId { get; set; }

        public int DocId { get; set; }

        public string Desc { get; set; }

        public string Type { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string FileName { get; set; }

    }
}