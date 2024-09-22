using DesDer.Models;
using Microsoft.EntityFrameworkCore;

namespace DesDer.Services;

public class ImageService : IImageService
{
    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _environment;
    public ImageService(ApplicationContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    public async Task<FileModel[]> GetImages(string path)
    {
        return await _context.Files.Where(x => x.Path.StartsWith(path) && (x.Name.EndsWith(".jpg") || x.Name.EndsWith(".png")))
                                   .ToArrayAsync();
    }

    public async Task<FileStream> PrepareStream(string name, string path)
    {
        var file = new FileModel
        {
            Name = name,
            Path = $"/{path}/{name}"
        };

        _context.Files.Add(file);
        await _context.SaveChangesAsync();

        return new FileStream(_environment.WebRootPath + file.Path, FileMode.CreateNew);
    }
}
