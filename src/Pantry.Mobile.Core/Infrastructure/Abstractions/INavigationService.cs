namespace Pantry.Mobile.Core.Infrastructure.Abstractions;

public interface INavigationService
{
    Task<string> GetNextStartupPage();

    Task GoToAsync(ShellNavigationState state);

    Task GoToAsync(ShellNavigationState state, bool animate);

    Task GoToAsync(ShellNavigationState state, IDictionary<string, object> parameters);

    Task GoToAsync(ShellNavigationState state, bool animate, IDictionary<string, object> parameters);
}
