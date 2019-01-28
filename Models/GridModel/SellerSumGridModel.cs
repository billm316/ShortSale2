
namespace ShortSale2.Models
{
    public class SellerSumGridModel
    {
        public string Id { get; set; }

        public string PropertyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        //[StringLength(10)]
        //public string PhoneHome { get; set; }

        //[StringLength(10)]
        //public string PhoneWork { get; set; }

        //[StringLength(10)]
        //public string PhoneCell { get; set; }

        //[StringLength(10)]
        //public string PhoneFax { get; set; }

        //[StringLength(100)]
        public string Email { get; set; }

        public string AutoStatusUpdate { get; set; }

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