using System.Linq.Expressions;
using System.Text.Json;
using DesDer.Models;
using DesDer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DesDer.Pages;

public class DesDerPageModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    public Post Post { get; set; }
    public string? Content { get; set; } 
    public string? Tables { get; set; } 
    public string FullPath { get; set; }
    private readonly IPostService _postService;
    private readonly RoutesService _routesService;
    private readonly ICustomTableService _customTableService;

    public DesDerPageModel(IPostService postService, RoutesService routesService, ICustomTableService customTableService)
    {
        _postService = postService;
        _routesService = routesService;
        _customTableService = customTableService;
    }

    public async Task OnGetAsync()
    {
        Post = await _postService.GetPost(Id);
        var culture = RouteData.Values["culture"] as string;
        try
        {
            FullPath = _routesService.NavNode.GetPathTo(Guid.Parse(Post.PathGuid)).Remove(0, 5);
        }
        catch
        {
            FullPath = "";
        }
        
        switch(culture)
        {
            case "en":
                Content = Post.En;
                Tables = SelectTable(x => new { x.Id, Name = x.EnName });
                break;
            case "ru":
                Content = Post.Ru;
                Tables = SelectTable(x => new { x.Id, Name = x.RuName });
                break;
            case "kg":
                Content = Post.Kg;
                Tables = SelectTable(x => new { x.Id, Name = x.KgName });
                break;
        }
        Content = Content?.Replace("\\\"", "\\\\\"");
    }

    private string SelectTable<T>(Expression<Func<CustomTable, T>> lang)
    {
        return JsonSerializer.Serialize(_customTableService.GetAllTables().Select(lang));
    }
}