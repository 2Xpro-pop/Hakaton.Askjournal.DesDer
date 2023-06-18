using Destinationosh.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Destinationosh.Components;

public class HeaderSection : ViewComponent
{
    public IViewComponentResult Invoke(string header, string description, string culture)
    {
        return View(new { Header = header, Description = description, Culture = culture});
    }
}