using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.Links.Contracts;

namespace TinyLink.Core.ApplicationServices.Links
{
    public class UrlHasherGuid : IUrlHasher, ISingletonLifetime
    {
        public Task<string> Hash(string url)
        {
            return Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}
