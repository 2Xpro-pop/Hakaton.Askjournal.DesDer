using DesDer.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesDer.Controllers;


[Route("[controller]")]
public class EditorController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly IPostService _postService;
    private readonly ILogger<EditorController> _logger;

    public EditorController(IWebHostEnvironment environment, IPostService postService, ILogger<EditorController> logger)
    {
        _environment = environment;
        _postService = postService;
        _logger = logger;
    }

    [HttpPost("imageUpload")]
    public IActionResult ImageUpload(IFormFile image = null!)
    {
        if(image.Length > 1024 * 1024 * 2)
        {
            return BadRequest("Image size is too big");
        }
        else
        {
            try
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgs", imageName);
                var url = Url.Content($"~/imgs/{imageName}");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    image.CopyTo(stream);

                    
                }
                return Ok(new { Success = 1, File = new { Url = url } });
            }
            catch(Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }
    }

    [HttpPost("fetchUrl")]
    public IActionResult ImageUrl(string url)
    {
        var thisUrl = HttpContext.Request.Scheme +"://"+ HttpContext.Request.Host + "/";
        _logger.LogInformation($"{url}");
        if(thisUrl.StartsWith(url))
        {
            return Ok(new { Success = 1, File = new { Url = url } });
        }
        else
        {
            return BadRequest("Invalid url");
        }
    }

    [HttpPost("savePost")]
    public async Task<IActionResult> SavePostAsync(string culture, int id, string content)
    {
        try
        {
            var post = await _postService.GetPost(id);
            switch(culture)
            {
                case "en":
                    post.En = content;
                    break;
                case "ru":
                    post.Ru = content;
                    break;
                case "kg":
                    post.Kg = content;
                    break;
            }
            await _postService.SavePost(post);
            return Ok(new { Success = 1 });
        }
        catch(Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
}