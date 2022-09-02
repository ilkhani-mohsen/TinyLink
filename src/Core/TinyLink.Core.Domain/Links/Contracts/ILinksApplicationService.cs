using TinyLink.Common.Core.ApplicationServices;
using TinyLink.Core.Domain.Links.Models;

namespace TinyLink.Core.Domain.Links.Contracts
{
    public interface ILinksApplicationService
    {
        Task<ApplicationServiceResult<CreateLinkResponse>> CreateLink(CreateLinkRequest request);
        Task<ApplicationServiceResult<GetLinkUrlByCodeResponse>> GetLinkUrlByCode(GetLinkUrlByCodeRequest request);
    }
}
