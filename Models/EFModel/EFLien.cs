using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortSale2.Models
{
    public class EFLien
    {
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Missing required primary key")]
        public int Id { get; set; }

        //[ForeignKey("PropertyId")]
        [Required(ErrorMessage = "Missing required foriegn key")]
        public int PropertyId { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string Type { get; set; }

        public string Servicer { get; set; }

        public string AccountNo { get; set; }

        public string LienPosition { get; set; }

        public decimal SettlementAmt { get; set; }

        public DateTime? SettlementDate { get; set; }

        public string Status { get; set; }
        
        // Lien Details
        public decimal? MinNetProceeds { get; set; }

        public decimal? MaxNetProceeds { get; set; }

        public decimal? Valuation { get; set; }

        public DateTime? ValuationDate { get; set; }

        public decimal? PayoffAmt { get; set; }

        public DateTime? PayoffDate { get; set; }

        // 2nd line get 6000 if FRAN or FRED otherwise 8500
        // HAFA or None
        public string SettlementProgram { get; set; }

        // net proceeds > 84-88% before 2nd lien payoff
        public string FHA { get; set; }

        public string Investor { get; set; }
    }
}