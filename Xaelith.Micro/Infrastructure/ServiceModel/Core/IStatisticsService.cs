namespace Xaelith.Micro.Infrastructure.ServiceModel.Core;

using Xaelith.Micro.Infrastructure.DataModel.Core;

public interface IStatisticsService : IXaelithService
{
    Statistics Data { get; }

    void RecalculateContentStatistics();

    void PostHit(Guid postId);
    void DashboardHit();

    void Save();
}