
namespace ShortSale2.Models
{
    public class LienSumGridModel
    {
        public LienSumGridModel()
        {
            Type = "Res";
            LienPosition = "1";
            SettlementAmt = "0";
            SettlementDate = "";
            Status = "Doc Coll";
            MaxNetProceeds = "0";
            MinNetProceeds = "0";
            Valuation = "0";
            ValuationDate = "";
            PayoffAmt = "0";
            PayoffDate = "";
            SettlementProgram = "Unk";
            FHA = "Unk";
            Investor = "Unk";
        }

        public string Id { get; set; }

        public string PropertyId { get; set; }

        public string ReviewDate { get; set; }

        // Res,Com,
        public string Type { get; set; }

        public string Servicer { get; set; }

        public string AccountNo { get; set; }

        public string LienPosition { get; set; }

        public string SettlementAmt { get; set; }

        public string SettlementDate { get; set; }

        public string Status { get; set; }

        // min amt a junior lien will accept
        public string MinNetProceeds { get; set; }

        // Max amt primary lien allows to junior liens
        public string MaxNetProceeds { get; set; }

        public string Valuation { get; set; }

        public string ValuationDate { get; set; }

        public string PayoffAmt { get; set; }

        public string PayoffDate { get; set; }

        // 2nd line get 6000 if FRAN or FRED otherwise 8500
        // HAFA,No,Unk
        public string SettlementProgram { get; set; }

        // 2nd lien mortgages get 8% payoff amount
        // Yes,No,Unk
        public string FHA { get; set; }

        // Fann,Fred,Private,Unk
        public string Investor { get; set; }
    }
}
