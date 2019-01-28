using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFContact
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing required primary key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int PropertyId { get; set; }

        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string Role { get; set; }

        //[StringLength(10)]
        //public string PhoneHome { get; set; }

        //[StringLength(10)]
        //public string PhoneWork { get; set; }

        //[StringLength(10)]
        //public string PhoneCell { get; set; }

        //[StringLength(10)]
        //public string PhoneFax { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        // set to true, a weekly email is sent with current short sale status report
        public Boolean AutoStatusUpdate { get; set; }

        //[StringLength(50)]
        //public string MailAddress { get; set; }

        //[StringLength(30)]
        //public string MailCity { get; set; }

        //[StringLength(2)]
        //public string MailState { get; set; }

        //[StringLength(9)]
        //public string MailZip { get; set; }

        //[StringLength(9)]
        //public string SSN { get; set; }
    }
}