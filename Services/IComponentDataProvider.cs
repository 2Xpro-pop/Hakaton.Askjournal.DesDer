using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IComponentDataProvider
{
    Task<string> GetConfig(string id); 
    Task<T?> GetConfig<T>(string id);
    Task UpdateConfig(string id, object config);
}
