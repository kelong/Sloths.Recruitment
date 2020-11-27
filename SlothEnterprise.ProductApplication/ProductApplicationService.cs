using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Sloths.Recruitment.Contracts;
using Sloths.Recruitment.Contracts.RequestModel;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public bool IsSubmittedApplicationForSelectiveInvoiceDiscountSuccess(
            SelectiveInvoiceDiscountRequestModel product, int companyNumber)
        {
            if (product == null)
            {
                return false;
            }

            return _selectInvoiceService.SubmitApplicationFor(
                companyNumber, product.InvoiceAmount, product.AdvancePercentage
            );
        }

        public bool IsSubmittedApplicationForConfidentialInvoiceDiscountSuccess(
            ConfidentialInvoiceDiscountRequestModel product, ISellerApplication application)
        {
            if (application.CompanyData == null || product == null)
            {
                return false;
            }

            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                new CompanyDataRequestModel
                {
                    CompanyFounded = application.CompanyData.Founded,
                    CompanyNumber = application.CompanyData.Number,
                    CompanyName = application.CompanyData.Name,
                    DirectorName = application.CompanyData.DirectorName
                }, product);

            return result.Success && result.ApplicationId.HasValue;
        }

        public bool IsSubmittedApplicationForBusinessLoansSuccess(
            BusinessLoans product, ISellerApplication application)
        {
            if (application?.CompanyData == null || application.Product == null || product == null)
            {
                return false;
            }

            var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequestModel
            {
                CompanyFounded = application.CompanyData.Founded,
                CompanyNumber = application.CompanyData.Number,
                CompanyName = application.CompanyData.Name,
                DirectorName = application.CompanyData.DirectorName
            }, new LoansRequestModel
            {
                InterestRatePerAnnum = product.InterestRatePerAnnum,
                LoanAmount = product.LoanAmount
            });
            return result.Success && result.ApplicationId.HasValue;
        }
    }
}
