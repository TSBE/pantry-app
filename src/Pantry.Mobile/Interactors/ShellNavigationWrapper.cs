using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Refit;

namespace Pantry.Mobile.Interactors;

public class ShellNavigationWrapper : INavigationService
{
    private readonly ISettingsService _settingsService;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IAuth0Client _auth0Client;


    public ShellNavigationWrapper(ISettingsService settingsService, IPantryClientApiService pantryClientApiService, IAuth0Client auth0Client)
    {
        _settingsService = settingsService;
        _pantryClientApiService = pantryClientApiService;
        _auth0Client = auth0Client;
    }
    public async Task<string> GetNextStartupPage(CancellationToken cancellationToken)
    {
        var targetPage = $"//{PageConstants.TABBAR_PAGE}";

        var onboardingHasBeenFinished = await _settingsService.GetOnboardingHasBeenFinished();
        if (!onboardingHasBeenFinished)
        {
            targetPage = $"//{PageConstants.ONBOARDING_PAGE}";
            return targetPage;
        }

        var loginCredentials = await _settingsService.GetCredentials();
        if (loginCredentials is { IsExpired: true, HasError: false })
        {
            loginCredentials = await _auth0Client.RefreshToken(loginCredentials.RefreshToken, cancellationToken);
            await _settingsService.SetCredentials(loginCredentials);
        }

        if (loginCredentials.HasError || loginCredentials.IsExpired)
        {
            targetPage = $"//{PageConstants.LOGIN_PAGE}";
            return targetPage;
        }

        var account = await GetAccount();
        if (account is null)
        {
            targetPage = $"//{PageConstants.CREATEACCOUNT_PAGE}";
            return targetPage;
        }

        if (account.Household is null)
        {
            targetPage = $"//{PageConstants.CREATEHOUSEHOLD_PAGE}";
            return targetPage;
        }

        return targetPage;
    }

    public Task GoToAsync(string state) => Shell.Current.GoToAsync(state);

    public Task GoToAsync(string state, bool animate) => Shell.Current.GoToAsync(state, animate);

    public Task GoToAsync(string state, IDictionary<string, object> parameters) => Shell.Current.GoToAsync(state, parameters);

    public Task GoToAsync(string state, bool animate, IDictionary<string, object> parameters) => Shell.Current.GoToAsync(state, animate, parameters);


    private async Task<AccountResponse?> GetAccount()
    {
        try
        {
            return await _pantryClientApiService.GetAccountAsync();
        }
        catch (ValidationApiException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
