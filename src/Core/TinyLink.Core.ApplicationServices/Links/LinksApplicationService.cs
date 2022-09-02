using TinyLink.Common.Core.ApplicationServices;
using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.Links.Contracts;
using TinyLink.Core.Domain.Links.Entities;
using TinyLink.Core.Domain.Links.Models;

namespace TinyLink.Core.ApplicationServices.Links
{
    public class LinksApplicationService : ApplicationService<LinksApplicationService>, ILinksApplicationService, IScopedLifetime
    {
        private readonly ILinksRepository linksRepository;
        private readonly IUrlHasher urlHasher;

        public LinksApplicationService(ApplicationServiceContext context, ILinksRepository linksRepository, IUrlHasher urlHasher) : base(context)
        {
            this.linksRepository = linksRepository;
            this.urlHasher = urlHasher;
        }

        public async Task<ApplicationServiceResult<CreateLinkResponse>> CreateLink(CreateLinkRequest request)
        {
            return await Execute(request, CreateLinkHandler);
        }

        private async Task<CreateLinkResponse> CreateLinkHandler(CreateLinkRequest request) 
        {
            var link = new Link()
            {
                Url = request.Url,
                CreatedAt = DateTime.Now,
                IsVisible = true,
                TotalVistCount = 0
            };
            link.Code = await urlHasher.Hash(request.Url);

            await linksRepository.AddLink(link);

            return new CreateLinkResponse()
            {
                Code = link.Code
            };
        }
    }
}
