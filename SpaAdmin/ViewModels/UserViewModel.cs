using System.ComponentModel.DataAnnotations;
using DesDer.Models;

namespace DesDer.SpaAdmin.ViewModels;

public class UserViewModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Editor;
}