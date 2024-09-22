using Microsoft.AspNetCore.Mvc;

namespace DesDer.Components;

public class SubBar : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var fullPath = HttpContext.Request.Path.Value;
        var culture =  fullPath.Split("/")[1];
        var subPath = fullPath.Split("/")[2];
        return View(new {Culture = culture, FullPath = fullPath, SubPath = subPath});
    }
}