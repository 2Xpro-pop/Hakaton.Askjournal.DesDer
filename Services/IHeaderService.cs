using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IHeaderService 
{
    Task Init();
    Task<FileModel[]> GetHeaderImages();

    Task<FileStream> PrepareStream(string name);

    FileModel SelectedImage { get; set; }

    string TextColor { get; set; }
}
