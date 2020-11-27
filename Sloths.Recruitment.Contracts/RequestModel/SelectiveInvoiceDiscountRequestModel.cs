namespace Sloths.Recruitment.Contracts.RequestModel
{
    public class SelectiveInvoiceDiscountRequestModel : IProduct
    {
        /// <summary>
        /// Id of the invoice
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Proposed networth of the Invoice
        /// </summary>
        public decimal InvoiceAmount { get; set; }

        /// <summary>
        /// Percentage of the networth agreed and advanced to seller
        /// </summary>
        public decimal AdvancePercentage { get; set; } = 0.80M;
    }
}
