using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Destinationosh.Components;

public class FooterSection : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var fullPath = HttpContext.Request.Path.Value;
        var culture = fullPath.Split("/")[1];
        var subPath = fullPath.Split("/")[2];
        return View(new FooterSectionConfig { Culture = culture, Nodes = new string[] {"What to do", "Plan your Trips", "About us" } });
    }
}

public class FooterSectionConfig
{
    public string Culture { get; set; }
    public string[] Nodes { get; set; }
}