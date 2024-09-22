using DesDer.Models;

namespace DesDer.Services;

public interface IFileUploaderService
{
    Task<FileStream> PrepareStream(string name, string path);
}