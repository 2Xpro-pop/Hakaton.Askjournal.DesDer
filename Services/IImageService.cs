using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IImageService
{
    Task<FileStream> PrepareStream(string name, string path);
    Task<FileModel[]> GetImages(string path); 
}
