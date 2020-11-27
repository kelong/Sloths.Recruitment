using Sloths.Recruitment.Contracts.StaticValues;

namespace Sloths.Recruitment.Contracts.RequestModel
{
    public class ConfidentialInvoiceDiscountRequestModel : IProduct
    {
        public int Id { get; set; }
        public decimal TotalLedgerNetworth { get; set; }
        public decimal AdvancePercentage { get; set; }
        public decimal VatRate { get; set; } = VatRates.UkVatRate;
    }
}
