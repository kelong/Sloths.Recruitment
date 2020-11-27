using System;

namespace Sloths.Recruitment.Contracts.RequestModel
{
    public class CompanyDataRequestModel
    {
        public string CompanyName { get; set; }
        public int CompanyNumber { get; set; }
        public string DirectorName { get; set; }
        public DateTime CompanyFounded { get; set; }
    }
}
