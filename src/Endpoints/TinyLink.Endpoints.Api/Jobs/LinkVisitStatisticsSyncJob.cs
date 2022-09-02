using Quartz;
using TinyLink.Core.Domain.LinkVisitStatistics.Contracts;

namespace TinyLink.Endpoints.Api.Jobs
{
    public class LinkVisitStatisticsSyncJob : IJob
    {
        private readonly ILinkVisitStatisticsRepository linkVisitStatisticsRepository;

        public LinkVisitStatisticsSyncJob(ILinkVisitStatisticsRepository linkVisitStatisticsRepository)
        {
            this.linkVisitStatisticsRepository = linkVisitStatisticsRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await linkVisitStatisticsRepository.Sync();
        }
    }
}
