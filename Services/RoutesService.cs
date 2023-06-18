using System.Text;
using System.Text.Json;
using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;
public class RoutesService
{
    private static readonly Dictionary<string, NavNode> _memo = new();
    private static readonly Dictionary<object, string> _pathes = new();


    private readonly int _id;
    private readonly NavNode _navNode;
    private readonly ApplicationContext _context;

    public NavNode NavNode => _navNode;

    public RoutesService(ApplicationContext context)
    {
        _context = context;
        var routes = _context.Routes.FirstOrDefault();
        if(routes == null)
        {
            _navNode = CreateDefault();
            _context.Routes.Add(new RouteModel()
            {
                Name = JsonSerializer.Serialize(_navNode)
            });
            _context.SaveChanges();
            return;
        }
        _navNode =  JsonSerializer.Deserialize<NavNode>(routes.Name) ?? CreateDefault();
    }

    public async Task<Post?> GetPostByRoute(string route)
    {
        string id;
        if (_memo.ContainsKey(route))
        {
            id = _memo[route].Id.ToString();
            return await _context.Posts.FirstOrDefaultAsync(post => post.PathGuid == id);
        }

        NavNode? node = NavNode;
        if (!(string.IsNullOrWhiteSpace(route)
           || string.Equals(route, "main", StringComparison.OrdinalIgnoreCase)))
        {
            foreach (var pathFragment in route.Split('/'))
            {
                if (string.IsNullOrWhiteSpace(pathFragment))
                {
                    return null;
                }
                node = node[pathFragment];
                if (node == null)
                {
                    return null;
                }
            }
        }

        _memo[route] = node;
        
        id = node.Id.ToString();
        return await _context.Posts.FirstOrDefaultAsync(post => post.PathGuid == id);
    }

    public void SaveChanges()
    {
        var routes = _context.Routes.First();
        routes.Name = JsonSerializer.Serialize(_navNode);
        _context.Routes.Update(
            routes
        );
        _context.SaveChanges();
    }

    public string GetPathTo(NavNode node)
    {
        string path;

        if(!_pathes.TryGetValue(node, out path))
        {
            path = NavNode.GetPathTo(node.Id);
        }

        return path;
    }

    public string GetPathTo(Guid node)
    {
        string path;

        if (!_pathes.TryGetValue(node, out path))
        {
            path = NavNode.GetPathTo(node);
        }

        return path;
    }
    
    NavNode CreateDefault () => new NavNode 
    {
        Id = Guid.NewGuid(),
        KgName = "Главная",
        RuName = "Главная",
        EnName = "Main",
        SubPath = "Main",
        Children = new List<NavNode>()
        {
            new NavNode()
            {
                Id = Guid.NewGuid(),
                KgName = "О нас",
                RuName = "О нас",
                EnName = "About us",
                SubPath = "About",
            },
            new NavNode()
            {
                Id = Guid.NewGuid(),
                KgName = "Контакты",
                RuName = "Контакты",
                EnName = "Contacts",
                SubPath = "Contacts",
            }
        }
    };
}