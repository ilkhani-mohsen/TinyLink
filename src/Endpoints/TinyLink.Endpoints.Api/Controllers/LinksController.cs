using Microsoft.AspNetCore.Mvc;
using TinyLink.Common.AspNetCore;
using TinyLink.Core.Domain.Links.Contracts;
using TinyLink.Core.Domain.Links.Models;

namespace TinyLink.Endpoints.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LinksController : BaseController
    {
        private readonly ILinksApplicationService linksApplicationService;

        public LinksController(ILinksApplicationService linksApplicationService)
        {
            this.linksApplicationService = linksApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLink(CreateLinkRequest request) 
        {
            var result = await linksApplicationService.CreateLink(request);
            return Ok(result);
        }
    }
}
