using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class HeaderService : IHeaderService
{
    private static FileModel _selectedImage = null!;
    private static string _color = "#212529";

    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly HttpClient _httpClient;
    public HeaderService(ApplicationContext context, IWebHostEnvironment environment, IImageService imageService, HttpClient httpClient)
    {
        _context = context;
        _environment = environment;
        _imageService = imageService;
        _httpClient = httpClient;
    }

    public FileModel SelectedImage 
    {
        get
        {
            return _selectedImage;
        }
        
        set => _selectedImage = value;
    }

    public string TextColor
    { 
        get => _color; 
        set => _color = value;
    }

    public async Task<FileModel[]> GetHeaderImages()
    {
        var array = await _imageService.GetImages("/imgs/banner/header");
        if (_selectedImage == null)
        {
            _selectedImage = array[0];
        }
        return array;
    }

    public async Task Init()
    {
        await GetHeaderImages();
    }

    public Task<FileStream> PrepareStream(string name)
    {
        return _imageService.PrepareStream(name, "imgs/banner/header");
    }
}
