using System.Text.Json;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Auth0;

namespace Pantry.Mobile.Core.Infrastructure;

public class SettingsService : ISettingsService
{
    private const string ONBOARDING = "OnBoardingHasBeenFinished";
    private const string CREDENTIALS = "Credentials";

    private readonly IPreferences _preferences;
    private readonly ISecureStorage _secureStorage;

    public SettingsService(IPreferences preferences, ISecureStorage secureStorage)
    {
        _preferences = preferences;
        _secureStorage = secureStorage;
    }

    /// <summary>
    /// First use generates installation Id.
    /// returns installation Id.
    /// </summary>
    public Guid InstallationId
    {
        get
        {
            if (!Guid.TryParse(_preferences.Get(nameof(InstallationId), string.Empty), out var installationId))
            {
                installationId = Guid.NewGuid();
                _preferences.Set(nameof(InstallationId), installationId.ToString());
            }
            return installationId;
        }
        private set => _preferences.Set(nameof(InstallationId), value.ToString());
    }

    /// <summary>
    /// Gets the onboarding has been finished flag.
    /// </summary>
    /// <returns>true.</returns>
    public async Task<bool> GetOnboardingHasBeenFinished()
    {
        var value = await _secureStorage.GetAsync(ONBOARDING);
        _ = bool.TryParse(value, out var onboardingHasBeenFinished);
        return onboardingHasBeenFinished;
    }

    /// <summary>
    /// Set the onboarding has been finished flag.
    /// </summary>
    public async Task SetOnboardingHasBeenFinished(bool value)
    {
        await _secureStorage.SetAsync(ONBOARDING, value.ToString());
    }

    /// <summary>
    /// Gets the credentials.
    /// </summary>
    /// <returns>credentials.</returns>
    public async Task<Credentials> GetCredentials()
    {
        var json = await _secureStorage.GetAsync(CREDENTIALS);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new() { Error = "not logged in" };
        }

        var credentials = JsonSerializer.Deserialize<Credentials>(json);
        if (credentials is null)
        {
            return new() { Error = "not logged in" };
        }

        return credentials;
    }

    /// <summary>
    /// Set the credentials.
    /// </summary>
    public async Task SetCredentials(Credentials credentials)
    {
        var json = JsonSerializer.Serialize(credentials);
        await _secureStorage.SetAsync(CREDENTIALS, json);
    }

    /// <summary>
    /// Delete credentials.
    /// </summary>
    /// <returns>credentials.</returns>
    public Task DeleteCredentials()
    {
        _secureStorage.Remove(CREDENTIALS);
        return Task.CompletedTask;
    }
}
