using System.Text.Json;
using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class ComponentDataProvider : IComponentDataProvider
{
    private readonly ApplicationContext _context;
    public ComponentDataProvider(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetConfig<T>(string id) 
    {
        var config = await _context.ComponentConfigs.FirstOrDefaultAsync(config => config.Id == id);
        T? result = default;

        if(config == null)
        {
            _context.ComponentConfigs.Add(new ComponentConfig
            {
                Id = id,
                Config = "{}"
            });
        }
        else
        {
            result = JsonSerializer.Deserialize<T>(config.Config);
        }
        
        return result;
    }

    public async Task<string> GetConfig(string id)
    {
        var config = await _context.ComponentConfigs.FirstOrDefaultAsync(config => config.Id == id);
        var result = "{}";

        if (config == null)
        {
            _context.ComponentConfigs.Add(new ComponentConfig
            {
                Id = id,
                Config = "{}"
            });
        }
        else
        {
            result = config.Config;
        }

        return result;
    }

    public async Task UpdateConfig(string id, object config)
    {
        var conf = await _context.ComponentConfigs.FirstOrDefaultAsync(config => config.Id == id);
        bool hasComponent = conf != null;

        var componentConfig = new ComponentConfig
        {
            Id = id,
            Config = config.ToString(),
        };

        if (hasComponent)
        {
            conf.Config = config.ToString();
        }
        else
        {
            await _context.ComponentConfigs.AddAsync(componentConfig);
        }
        
        await _context.SaveChangesAsync();
    }
}
