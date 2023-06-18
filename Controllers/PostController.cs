using Destinationosh.Models;
using Destinationosh.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Generic;

namespace Destionationosh.Controllers;

[Route("[controller]")]
public class PostController: ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly RoutesService _routesService;
    private readonly IPostAnalyticsService _postAnalytics;
    private readonly IMemoryCache _cache;
    public PostController(ApplicationContext context,
                          RoutesService routesService,
                          IPostAnalyticsService postAnalytics,
                          IMemoryCache cache)
    {
        _context = context;
        _routesService = routesService;
        _postAnalytics = postAnalytics;
        _cache = cache;
    }
    
    [HttpGet("get-post-by-route/{*route}")]
    public async Task<IActionResult> GetPostByRoute(string route)
    {
        var culture = route[..2];
        var routeName = route[3..];

        var post = await _routesService.GetPostByRoute(routeName);

        if (post == null)
        {
            return NotFound();
        }
        
        await _postAnalytics.AddPostVisit(post);

        var data = culture switch
        {
            "ru" => post.Ru,
            "en" => post.En,
            "kg" => post.Kg,
            _ => "{}",
        };

        return Ok(data);
    }
    [HttpGet("get-pages/{culture?}")]
    public async Task<IActionResult> GetPages(string? culture = null)
    {
        Dictionary<string, object> pages;
        if (!_cache.TryGetValue($"posts_{culture}", out pages))
        {
            pages = new();
            var posts = culture switch
            {
                "ru" => await _context.Posts.Select(p => new { Route = p.PathGuid, Post = p.Ru, Header = p.RuHeader, Description = p.RuDescription }).ToListAsync(),
                "en" => await _context.Posts.Select(p => new { Route = p.PathGuid, Post = p.En, Header = p.EnHeader, Description = p.EnDescription }).ToListAsync(),
                "kg" => await _context.Posts.Select(p => new { Route = p.PathGuid, Post = p.Kg, Header = p.KgHeader, Description = p.KgDescription }).ToListAsync(),
                _ => await _context.Posts.Select(p => new { Route = p.PathGuid, Post = p.En, Header = p.EnHeader, Description = p.EnDescription }).ToListAsync(),
            };

            foreach (var post in posts)
            {
                var path = _routesService.GetPathTo(Guid.Parse(post.Route));
                if(string.IsNullOrWhiteSpace(path))
                {
                    continue;
                }
                pages.Add(path, new { post.Post, post.Header, post.Description });
            }

            _cache.Set($"posts_{culture}", pages, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            });
        }
        
        return Ok(pages);
    }
}


