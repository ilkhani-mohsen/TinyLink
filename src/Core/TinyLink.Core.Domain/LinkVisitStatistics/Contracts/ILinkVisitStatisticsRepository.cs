namespace TinyLink.Core.Domain.LinkVisitStatistics.Contracts
{
    public interface ILinkVisitStatisticsRepository
    {
        Task Sync();
    }
}
