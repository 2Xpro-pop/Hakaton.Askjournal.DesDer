using DesDer.Models;

namespace DesDer.Services;

public class PostContainerService: IPostContainerService
{
    public IList<Post> Posts { get; } = new List<Post>();
}