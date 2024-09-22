using DesDer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace DesDer.Controllers;

[Route("api/[controller]")]
public class ComponentController : ControllerBase
{
    private readonly IComponentDataProvider _componentService;
    private readonly KostylTokenProvider _tokenProvider;
    public ComponentController(IComponentDataProvider componentDataProvider, KostylTokenProvider tokenProvider)
    {
        _componentService = componentDataProvider;
        _tokenProvider = tokenProvider;
    }

    [HttpPost("update-config/{token}")]
    public async Task UpdateConfigAsync(string token)
    {
        if (_tokenProvider.Tokens.Keys.First() == token)
        {
            var data = await JsonSerializer.DeserializeAsync<UpdateConfigData>(Request.Body);

            await _componentService.UpdateConfig(data.Id, data.Data);
        }
        
    }

    [HttpGet("get-config/{id}/{token}")]
    public async Task<IActionResult> GetConfigAsync(string id, string token)
    {
        if(_tokenProvider.Tokens.Keys.First() == token)
        {
            return Ok(await _componentService.GetConfig(id));
        }

        return BadRequest();
        
    }

    public struct UpdateConfigData
    {
        public string Id { get; set; }
        public string Data { get; set; }
    }

}
