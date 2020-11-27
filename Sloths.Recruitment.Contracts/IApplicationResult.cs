using System.Collections.Generic;

namespace Sloths.Recruitment.Contracts
{
    public interface IApplicationResult
    {
        int? ApplicationId { get; set; }
        bool Success { get; set; }
        IList<string> Errors { get; set; }
    }
}
