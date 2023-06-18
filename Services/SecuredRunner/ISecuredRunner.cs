using Microsoft.Extensions.Localization;

namespace Destinationosh.Services;

public interface ISecuredRunner
{
    ISecuredRunner OnError(Action<Exception> exc);
    ISecuredRunner OnSuccess(Action success);
    ISecuredRunner OnBadAuth(Action badAuth);
    ISecuredRunner OnBadRole(Action badRole);
    ISecuredRunner SetLocalizer(IStringLocalizer localizer);
    ISecuredRunner SetRoles(params string[] roles);
    Task Run(Action action);
}