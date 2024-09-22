using DesDer.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace DesDer.Services;

public class HeaderRestServices : IHeaderService
{
    private const string HeaderId = "HeaderComponent";

    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IImageService _imageService;
    private readonly IComponentDataProvider _componentDataProvider;
    private readonly HttpClient _httpClient;
    private readonly KostylTokenProvider _tokenProvider;

    public HeaderRestServices(ApplicationContext context,
                              IWebHostEnvironment environment,
                              IImageService imageService,
                              IComponentDataProvider componentDataProvider,
                              IHttpClientFactory clientFactory,
                              KostylTokenProvider tokenProvider)
    {
        _context = context;
        _environment = environment;
        _imageService = imageService;
        _httpClient = clientFactory.CreateClient();
        _componentDataProvider = componentDataProvider;
        _tokenProvider = tokenProvider;
    }


    public FileModel SelectedImage 
    {
        get => _selectedImage;
        set
        {
            _selectedImage = value;
            PostSend();
        }
    }
    private static FileModel? _selectedImage = null;

    public string TextColor 
    { 
        get => _color; 
        set
        {
            _color = value;
            PostSend();
        }
    }
    private static string? _color = null;

    public async Task<FileModel[]> GetHeaderImages()
    {
        var array = await _imageService.GetImages("/imgs/banner/header");
        if (_selectedImage == null)
        {
            _selectedImage = array[0];
        }
        return array;
    }

    public Task<FileStream> PrepareStream(string name)
    {
        return _imageService.PrepareStream(name, "imgs/banner/header");
    }

    private async void PostSend()
    {
        var data = new { SelectedImage = _selectedImage, TextColor = _color };

        var json = JsonSerializer.Serialize(data);

        if(_tokenProvider.Tokens.Count == 0)
        {
            _tokenProvider.Tokens.Add(Guid.NewGuid().ToString(), DateTime.Now);
        }

        var address = $"https://localhost:7129/api/component/update-config/{_tokenProvider.Tokens.Keys.First()}";

        var response = await _httpClient.PostAsJsonAsync(address, new { Id = HeaderId, Data = json });

        if(response.IsSuccessStatusCode)
        {

        }
    }

    public async Task Init()
    {
        if (_selectedImage == null)
        {
            var model = JsonSerializer.Deserialize<HeaderModel>(await _componentDataProvider.GetConfig(HeaderId));

            _selectedImage = model!.SelectedImage;
            _color = model.TextColor;
        }
    }

    class HeaderModel
    {
        public FileModel? SelectedImage { get; set; }
        public string TextColor { get; set; }
    }
}
