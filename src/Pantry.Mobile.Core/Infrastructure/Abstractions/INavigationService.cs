namespace Pantry.Mobile.Core.Infrastructure.Abstractions;

public interface INavigationService
{
    Task<string> GetNextStartupPage(CancellationToken cancellationToken);

    Task GoToAsync(string state);

    Task GoToAsync(string state, bool animate);

    Task GoToAsync(string state, IDictionary<string, object> parameters);

    Task GoToAsync(string state, bool animate, IDictionary<string, object> parameters);
}
