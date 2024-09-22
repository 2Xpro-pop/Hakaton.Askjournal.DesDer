using DesDer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesDer.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    
    [HttpGet("InRole/{roles}")]
    public IActionResult IsInRole(string roles)
    {
        _logger.LogInformation(roles);
        foreach(var role in roles.Split(','))
        {
            _logger.LogInformation(role);
            if(!_roleManager.RoleExistsAsync(role).Result)
            {
                return BadRequest($"Role {role} does not exist");
            }
            if(!User.IsInRole(role))
            {
                return BadRequest($"User is not in role {role}");
            }
        }
        return Ok();
    }
}