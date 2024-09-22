
using System.Net;
using DesDer.Models;
using MatBlazor;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace DesDer.Services;

public class MatToastSecuredRunner : ISecuredRunner
{
    public const string Api = "https://localhost:7129/api/Account/InRole";
    // Services
    private readonly IMatToaster _matToaster;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    // Properties
    private string[] _roles = new string[0];
    private Action _success;
    private Action<Exception> _exc;
    private Action _badAuth;
    private Action _badRole;
    private IStringLocalizer _localizer;

    public MatToastSecuredRunner(
        IMatToaster matToaster,
        HttpClient httpClient,
        ILogger<MatToastSecuredRunner> logger)
    {
        _matToaster = matToaster;
        _httpClient = httpClient;
        _logger = logger;
    }

    public ISecuredRunner OnError(Action<Exception> exc)
    {
        _exc = exc;
        return this;
    }

    public ISecuredRunner OnSuccess(Action success)
    {
        _success = success;
        return this;
    }

    public ISecuredRunner OnBadAuth(Action badAuth)
    {
        _badAuth = badAuth;
        return this;
    }

    public ISecuredRunner OnBadRole(Action badRole)
    {
        _badRole = badRole;
        return this;
    }
    public ISecuredRunner SetLocalizer(IStringLocalizer localizer)
    {
        _localizer = localizer;
        return this;
    }

    public ISecuredRunner SetRoles(params string[] roles)
    {
        _roles = roles;
        return this;
    }

    public async Task Run(Action action)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Api}/{string.Join(",", _roles)}");
            var response = await _httpClient.SendAsync(request);
            _logger.LogInformation($"{Api}/{string.Join(",", _roles)} {response.RequestMessage}");
            if(response.StatusCode == HttpStatusCode.Redirect)
            {
                _matToaster.Add(_localizer["Warning"], MatToastType.Warning, _localizer["NotAuthorized"]);
                _logger.LogWarning("User tried to perform an action without being logged in.");
                _badAuth?.Invoke();
                return;
            }
            if(response.StatusCode == HttpStatusCode.Forbidden)
            {
                _matToaster.Add(_localizer["Warning"], MatToastType.Warning, _localizer["NotAuthorized"]);
                _logger.LogWarning("User tried to perform an action without being in the correct role.");
                _badRole?.Invoke();
                return;
            }
            _logger.LogInformation($"{response.StatusCode} - {response}");
            action();
        }
        catch (Exception exc)
        {
            _logger.LogError(exc.Message);
            _matToaster.Add(_localizer["Error"], MatToastType.Danger, exc.Message);
            _exc?.Invoke(exc);
            return;
        }
        _success?.Invoke();
    }
}