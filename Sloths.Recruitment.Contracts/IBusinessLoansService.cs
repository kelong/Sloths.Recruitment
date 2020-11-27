using Sloths.Recruitment.Contracts.RequestModel;

namespace Sloths.Recruitment.Contracts
{
    /// <summary>
    /// Assume this is an external service. you cannot modify this interface
    /// </summary>
    public interface IBusinessLoansService
    {
        IApplicationResult SubmitApplicationFor(CompanyDataRequestModel applicantData, LoansRequestModel businessLoans);
    }
}
