
namespace ShortSale2.Models
{
    public class LienDetailGridModel
    {
        public string Id { get; set; }

        public string PropertyId { get; set; }

        public string Type { get; set; }

        public string Servicer { get; set; }

        public string AccountNo { get; set; }

        public string LienPosition { get; set; }

        public string  SettlementAmt { get; set; }

        public string  SettlementDate { get; set; }

        public string Status { get; set; }

        // Lien Details

        // Minimum proceeds a lien request
        public string  MinNetProceeds { get; set; }

        // Maximum allowed amt from primary lien
        public string  MaxNetProceeds { get; set; }

        // BPO or appraisal amt
        public string  Valuation { get; set; }

        public string  ValuationDate { get; set; }

        public string  PayoffAmt { get; set; }

        public string  PayoffDate { get; set; }

        // 2nd line get 6000 if FRAN or FRED otherwise 8500
        public string HAFA { get; set; }

        // 2nd lien mortgages get 8% payoff amount
        public string FHA { get; set; }

        public string Fann { get; set; }

        public string Fred { get; set; }

    }
}