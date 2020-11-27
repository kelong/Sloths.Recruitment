namespace Sloths.Recruitment.Contracts
{
    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface ISelectInvoiceService
    {
        bool SubmitApplicationFor(int companyNumber, decimal invoiceAmount, decimal advancePercentage);
    }
}
