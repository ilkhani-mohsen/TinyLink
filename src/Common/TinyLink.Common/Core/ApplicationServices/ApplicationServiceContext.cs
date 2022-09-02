using TinyLink.Common.Dependency;

namespace TinyLink.Common.Core.ApplicationServices
{
    public class ApplicationServiceContext
    {
        public IServiceProvider ServiceProvider { get; set; }
        public ApplicationServiceContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
