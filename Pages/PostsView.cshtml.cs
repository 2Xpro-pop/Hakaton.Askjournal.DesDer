using DesDer.Models;
using DesDer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DesDer.Pages;

public class PostsViewPageModel : PageModel
{
    private readonly RoutesService _routesService;
    private readonly ApplicationContext _context;
    private readonly ICustomTableService _tableService;
    private readonly IPostAnalyticsService _postAnalytics;
    private readonly IHeaderService _headerService;
    public string Route { get; set; } = "";
    public string Content { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public string Keywords { get; set; } = "Osh Ош";
    public string Culture { get; set; }
    public string PathToImage { get; set; }
    public string TextColor { get; set; }
    public string Data { get; set; }
    public dynamic Tabels { get; set; }
    public PostsViewPageModel(
        RoutesService routesService,
        ApplicationContext context,
        IPostAnalyticsService postAnalytics,
        IHeaderService headerService,
        ICustomTableService tableService)
    {
        _routesService = routesService;
        _context = context;
        _postAnalytics = postAnalytics;
        _headerService = headerService;
        _tableService = tableService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var culture = RouteData.Values["culture"] as string;
        var path = HttpContext.Request.Path.Value;
        Route = path.Substring(path.IndexOf("/", 1) + 1);
        var node = _routesService.NavNode;
        Culture = culture;
        if (!(string.IsNullOrWhiteSpace(Route)
           || string.Equals(Route, "main", StringComparison.OrdinalIgnoreCase)))
        {
            foreach (var pathFragment in Route.Split('/'))
            {
                if (string.IsNullOrWhiteSpace(pathFragment))
                {
                    return NotFound();
                }
                node = node[pathFragment];
                if (node == null)
                {
                    return NotFound();
                }
            }
        }
        
        var id = node.Id.ToString();
        var post = await _context.Posts.FirstOrDefaultAsync(post => post.PathGuid == id);
        if (post == null)
        {
            return NotFound();
        }
        await _postAnalytics.AddPostVisit(post);
        switch (culture)
        {
            case "ru":
                Content = post.Ru;
                Header = post.RuHeader;
                Description = post.RuDescription;
                break;
            case "en":
                Content = post.En;
                Header = post.EnHeader;
                Description = post.EnDescription;
                break;
            case "kg":
                Content = post.Kg;
                Header = post.KgHeader;
                Description = post.KgDescription;
                break;
        }

        if (string.IsNullOrWhiteSpace(Content))
        {
            return Page();
        }
        Data = Content;
        //Content = _postBuilder.Build(Content);

        if(_headerService.SelectedImage == null)
        {
            await _headerService.Init();
        }

        PathToImage = _headerService.SelectedImage?.Path;
        TextColor = _headerService.TextColor;
        Tabels = _tableService.GetAllTabelVms(culture);

        return Page();
    }
}