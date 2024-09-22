using DesDer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DesDer.Services;

public class PostService : IPostService
{
    private readonly ApplicationContext _db = null!;
    private readonly IMemoryCache _cache = null!;
    public PostService(ApplicationContext db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task AddPost(Post post)
    {
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();;
        RemoveCache();
    }

    public async Task<Post[]> GetPosts()
    {
        return await _db.Posts.Include(post => post.User).ToArrayAsync();
    }

    public async Task<Post> GetPost(int id)
    {
        return await _db.Posts.FirstAsync(p => p.Id == id);
    }

    public async Task RemovePost(Post post)
    {
        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        RemoveCache();
    }

    public async Task SavePost(Post post)
    {
        _db.Posts.Update(post);
        _db.Entry(post).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        RemoveCache();
    }

    private void RemoveCache()
    {
        _cache.Remove($"posts_ru");
        _cache.Remove($"posts_en");
        _cache.Remove($"posts_kg");
    }
}