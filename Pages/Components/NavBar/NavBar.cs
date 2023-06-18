using Microsoft.AspNetCore.Mvc;

namespace Destinationosh.Components;

public class NavBar : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var fullPath = HttpContext.Request.Path.Value;
        var culture = fullPath.Split("/")[1];
        return View(new {Culture = culture, PathWithoutCulture = fullPath.Substring(4)});
    }
}