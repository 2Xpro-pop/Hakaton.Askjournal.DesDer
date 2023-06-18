using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Destinationosh.SpaAdmin.ViewModels;

public class TableViewModelValidator: AbstractValidator<TableViewModel>
{
    public TableViewModelValidator(IStringLocalizer<TableViewModelValidator> localizer)
    {
        RuleFor(x => x.EnName).NotEmpty().WithMessage(localizer["NameRequired"]);
        RuleFor(x => x.KgName).NotEmpty().WithMessage(localizer["NameRequired"]);
        RuleFor(x => x.RuName).NotEmpty().WithMessage(localizer["NameRequired"]);
    }
}
