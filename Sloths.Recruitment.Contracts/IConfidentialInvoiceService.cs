using Sloths.Recruitment.Contracts.RequestModel;

namespace Sloths.Recruitment.Contracts
{
    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface IConfidentialInvoiceService
    {
        IApplicationResult SubmitApplicationFor(CompanyDataRequestModel applicantData, ConfidentialInvoiceDiscountRequestModel product);
    }
}
