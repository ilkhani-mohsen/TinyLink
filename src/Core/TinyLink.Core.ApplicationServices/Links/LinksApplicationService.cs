﻿using Microsoft.AspNetCore.Http;
using TinyLink.Common.Core.ApplicationServices;
using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.Links.Contracts;
using TinyLink.Core.Domain.Links.Entities;
using TinyLink.Core.Domain.Links.Models;
using TinyLink.Core.Domain.LinkVisits.Contracts;
using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Core.ApplicationServices.Links
{
    public class LinksApplicationService : ApplicationService<LinksApplicationService>, ILinksApplicationService, IScopedLifetime
    {
        private readonly ILinksRepository linksRepository;
        private readonly IUrlHasher urlHasher;
        private readonly ILinkVisitsLogStrategy linkVisitLogStrategy;

        public LinksApplicationService(ApplicationServiceContext context, ILinksRepository linksRepository, IUrlHasher urlHasher, ILinkVisitsLogStrategy linkVisitLogStrategy) : base(context)
        {
            this.linksRepository = linksRepository;
            this.urlHasher = urlHasher;
            this.linkVisitLogStrategy = linkVisitLogStrategy;
        }

        public async Task<ApplicationServiceResult<CreateLinkResponse>> CreateLink(CreateLinkRequest request)
        {
            return await Execute(request, CreateLinkHandler);
        }

        public async Task<ApplicationServiceResult<GetLinkUrlByCodeResponse>> GetLinkUrlByCode(GetLinkUrlByCodeRequest request)
        {
            return await Execute(request, GetLinkUrlByCodeHandler);
        }

        private async Task<ApplicationServiceResult<CreateLinkResponse>> CreateLinkHandler(CreateLinkRequest request) 
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

            var data = new CreateLinkResponse()
            {
                Code = link.Code
            };

            return Ok(data);
        }

        private async Task<ApplicationServiceResult<GetLinkUrlByCodeResponse>> GetLinkUrlByCodeHandler(GetLinkUrlByCodeRequest request) 
        {
            var link = await linksRepository.GetLinkByCode(request.Code);

            if (link == null || !link.IsVisible)
                return NotFound<GetLinkUrlByCodeResponse>("کد وارد شده معتبر نیست.");

            var linkVisit = new LinkVisit()
            {
                LinkId = link.Id,
                VisitedAt = DateTime.Now,
                IsProcessed = false
            };

            await linkVisitLogStrategy.Log(linkVisit);

            var data = new GetLinkUrlByCodeResponse()
            {
                Url = link.Url
            };

            return Ok(data);
        }
    }
}
