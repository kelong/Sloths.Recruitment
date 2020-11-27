using FluentAssertions;
using Moq;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Sloths.Recruitment.Contracts;
using Sloths.Recruitment.Contracts.RequestModel;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceTests
    {
        private readonly ProductApplicationService _productApplicationService;

        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();

        public ProductApplicationServiceTests()
        {
            _productApplicationService = new ProductApplicationService(_selectInvoiceServiceMock.Object,
                _confidentialInvoiceServiceMock.Object, _businessLoansServiceMock.Object);
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);
        }

        public static TheoryData<ConfidentialInvoiceDiscountRequestModel, SellerApplication> ValidConfidentialInvoiceDiscountRequestModelSellerApplications =
            new TheoryData<ConfidentialInvoiceDiscountRequestModel, SellerApplication>
            {
                {
                    new ConfidentialInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        VatRate = 1,
                        TotalLedgerNetworth = 1
                    },
                    new SellerApplication
                    {
                        Product = new BusinessLoans(),
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new ConfidentialInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        VatRate = 1,
                        TotalLedgerNetworth = 1
                    },
                    new SellerApplication
                    {
                        Product = new ConfidentialInvoiceDiscountRequestModel(),
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new ConfidentialInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        VatRate = 1,
                        TotalLedgerNetworth = 1
                    },
                    new SellerApplication
                    {
                        Product = new SelectiveInvoiceDiscountRequestModel(),
                        CompanyData = new SellerCompanyData()
                    }
                },
            };

        [Theory]
        [MemberData(nameof(ValidConfidentialInvoiceDiscountRequestModelSellerApplications))]
        public void Given_CompanyRequestModel_When_CalledWithConfidentialInvoiceDiscount_Then_ShouldReturnTrue(
            ConfidentialInvoiceDiscountRequestModel model, SellerApplication application)
        {
            _confidentialInvoiceServiceMock.Setup(
                m => m.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequestModel>(),
                    It.IsAny<ConfidentialInvoiceDiscountRequestModel>()
                )
            ).Returns(_result.Object);

            var result = _productApplicationService.IsSubmittedApplicationForConfidentialInvoiceDiscountSuccess(
                model, application
            );
            result.Should().Be(true);
        }

        public static TheoryData<ConfidentialInvoiceDiscountRequestModel, SellerApplication> InvalidConfidentialInvoiceDiscountRequestModelSellerApplications =
            new TheoryData<ConfidentialInvoiceDiscountRequestModel, SellerApplication>
            {
                {
                    new ConfidentialInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        VatRate = 1,
                        TotalLedgerNetworth = 1
                    },
                    new SellerApplication
                    {
                        Product = new BusinessLoans()
                    }
                },
                {
                    null,
                    new SellerApplication
                    {
                        Product = new ConfidentialInvoiceDiscountRequestModel(),
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new ConfidentialInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        VatRate = 1,
                        TotalLedgerNetworth = 1
                    },
                    new SellerApplication()
                },
            };

        [Theory]
        [MemberData(nameof(InvalidConfidentialInvoiceDiscountRequestModelSellerApplications))]
        public void Given_CompanyRequestModel_When_CalledWithInvalidConfidentialInvoiceDiscount_Then_ShouldReturnFalse(
            ConfidentialInvoiceDiscountRequestModel model, SellerApplication application)
        {
            _confidentialInvoiceServiceMock.Setup(
                m => m.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequestModel>(),
                    It.IsAny<ConfidentialInvoiceDiscountRequestModel>()
                )
            ).Returns(_result.Object);

            var result = _productApplicationService.IsSubmittedApplicationForConfidentialInvoiceDiscountSuccess(
                model, application
            );
            result.Should().Be(false);
        }

        public static TheoryData<SelectiveInvoiceDiscountRequestModel, int> ValidSelectiveInvoiceDiscountRequestModelSellerApplications =
            new TheoryData<SelectiveInvoiceDiscountRequestModel, int>
            {
                {
                    new SelectiveInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        InvoiceAmount = 1
                    },
                    1
                },
                {
                    new SelectiveInvoiceDiscountRequestModel(), 1
                },
                {
                    new SelectiveInvoiceDiscountRequestModel
                    {
                        Id = 1,
                        AdvancePercentage = 1,
                        InvoiceAmount = 1
                    },
                    0
                },
            };

        [Theory]
        [MemberData(nameof(ValidSelectiveInvoiceDiscountRequestModelSellerApplications))]
        public void Given_CompanyRequestModel_When_CalledWithSelectiveInvoiceDiscount_Then_ShouldReturnTrue(
            SelectiveInvoiceDiscountRequestModel model, int companyNumber)
        {
            _selectInvoiceServiceMock.Setup(_ => _.SubmitApplicationFor(
                    It.IsAny<int>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()
                )
            ).Returns(true);

            var result = _productApplicationService.IsSubmittedApplicationForSelectiveInvoiceDiscountSuccess(
                model, companyNumber
            );
            result.Should().Be(true);
        }

        [Fact]
        public void Given_InvalidCompanyRequestModel_When_CalledWithSelectiveInvoiceDiscount_Then_ShouldReturnFalse()
        {
            _selectInvoiceServiceMock.Setup(_ => _.SubmitApplicationFor(
                    It.IsAny<int>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()
                )
            ).Returns(true);

            var result = _productApplicationService.IsSubmittedApplicationForSelectiveInvoiceDiscountSuccess(
                null, 1
            );
            result.Should().Be(false);
        }

        public static TheoryData<BusinessLoans, SellerApplication> ValidBusinessLoansRequestModelSellerApplications =
            new TheoryData<BusinessLoans, SellerApplication>
            {
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    new SellerApplication
                    {
                        Product = new BusinessLoans(),
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    new SellerApplication
                    {
                        Product = new ConfidentialInvoiceDiscountRequestModel(),
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    new SellerApplication
                    {
                        Product = new SelectiveInvoiceDiscountRequestModel(),
                        CompanyData = new SellerCompanyData()
                    }
                },
            };

        [Theory]
        [MemberData(nameof(ValidBusinessLoansRequestModelSellerApplications))]
        public void Given_BusinessLoansModel_When_CalledWithConfidentialInvoiceDiscount_Then_ShouldReturnTrue(
            BusinessLoans model, SellerApplication application)
        {
            _businessLoansServiceMock.Setup(
                m => m.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequestModel>(),
                    It.IsAny<LoansRequestModel>()
                )
            ).Returns(_result.Object);

            var result = _productApplicationService.IsSubmittedApplicationForBusinessLoansSuccess(
                model, application
            );
            result.Should().Be(true);
        }

        public static TheoryData<BusinessLoans, SellerApplication> InvalidBusinessLoansRequestModelSellerApplications =
            new TheoryData<BusinessLoans, SellerApplication>
            {
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    new SellerApplication
                    {
                        Product = new BusinessLoans()
                    }
                },
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    new SellerApplication
                    {
                        CompanyData = new SellerCompanyData()
                    }
                },
                {
                    new BusinessLoans
                    {
                        Id = 1,
                        LoanAmount = 1,
                        InterestRatePerAnnum = 1
                    },
                    null
                }
            };

        [Theory]
        [MemberData(nameof(InvalidBusinessLoansRequestModelSellerApplications))]
        public void Given_InvalidBusinessLoansModel_When_CalledWithConfidentialInvoiceDiscount_Then_ShouldReturnTrue(
            BusinessLoans model, SellerApplication application)
        {
            _businessLoansServiceMock.Setup(
                m => m.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequestModel>(),
                    It.IsAny<LoansRequestModel>()
                )
            ).Returns(_result.Object);

            var result = _productApplicationService.IsSubmittedApplicationForBusinessLoansSuccess(
                model, application
            );
            result.Should().Be(false);
        }
    }
}
