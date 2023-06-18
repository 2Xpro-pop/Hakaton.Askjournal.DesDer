using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Destinationosh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Destinationosh.Services;

namespace Destinationosh.Pages;

public class AdminPageModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AdminPageModel> _logger;
    private readonly KostylTokenProvider _tokenProvider;
    public string Message { get; set; } = "";
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; } = null;
    
    public AdminPageModel(SignInManager<User> signInManager, ILogger<AdminPageModel> logger, KostylTokenProvider tokenProvider)
    {
        _signInManager = signInManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }

    public async Task<IActionResult> OnPostAsync(string login, string password, string? redirect)
    {
        if(await _signInManager.PasswordSignInAsync(login, password, false, false) != 
            Microsoft.AspNetCore.Identity.SignInResult.Success)

        {
            Message = "Неверный логин или пароль";
            return Page();
        }
        else
        {
            Message = "Вы успешно вошли";
            _tokenProvider.CreateToken();
            if(redirect != null)
            {
                return Redirect(redirect);
            }
            return Redirect(Request.PathBase+Request.Path);
        }
    }

    public async Task<IActionResult> OnGetLogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/admin");
    }

    public IActionResult OnGetChangeLanguage(string lang)
    {
        _logger.LogInformation(Request.PathBase+Request.Path);
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang, lang)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
        return Redirect(Request.PathBase+Request.Path);
    }
}