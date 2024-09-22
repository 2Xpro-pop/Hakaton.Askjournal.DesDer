using DesDer.Models;

namespace DesDer.Services;

public interface IPostAnalyticsService
{
    Task<PostVisit[]> GetPostVisits(Post post);
    Task AddPostVisit(Post post);
    Task<PostAnalytics> GetPostAnalytics(Post post);
}