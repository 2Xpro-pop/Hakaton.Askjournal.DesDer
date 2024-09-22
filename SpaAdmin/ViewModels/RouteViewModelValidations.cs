using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DesDer.SpaAdmin.ViewModels;

public class RouteViewModelValidator : AbstractValidator<RouteViewModel>
{
    public RouteViewModelValidator(IStringLocalizer<RouteViewModel> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(localizer["NameRequired"]);
    }

    bool ValidateRoute(string route)
    {
        if(string.IsNullOrWhiteSpace(route))
        {
            return false;
        }
        if(route.StartsWith("/") || route.EndsWith("/"))
        {
            return false;
        }
        if(!route.Split("/").All(x => x.Length > 0))
        {
            return false;
        }
        return true;
    }
}