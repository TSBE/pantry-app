using Pantry.Mobile.Core.Infrastructure.Auth0;

namespace Pantry.Mobile.Core.Infrastructure.Abstractions;

public interface ISettingsService
{
    Guid InstallationId { get; }

    Task<bool> GetOnboardingHasBeenFinished();

    Task SetOnboardingHasBeenFinished(bool value);

    Task<Credentials> GetCredentials();

    Task SetCredentials(Credentials credentials);
}
