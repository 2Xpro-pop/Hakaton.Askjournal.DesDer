using DesDer.Models;

namespace DesDer.Services;

public interface IPostContainerService
{
    IList<Post> Posts { get; }
}