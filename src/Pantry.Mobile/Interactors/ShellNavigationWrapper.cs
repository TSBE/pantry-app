using Pantry.Mobile.Core.Infrastructure.Abstractions;

namespace Pantry.Mobile.Interactors;

public class ShellNavigationWrapper : INavigationService
{
    public Task GoToAsync(ShellNavigationState state) => Shell.Current.GoToAsync(state);

    public Task GoToAsync(ShellNavigationState state, bool animate) => Shell.Current.GoToAsync(state, animate);

    public Task GoToAsync(ShellNavigationState state, IDictionary<string, object> parameters) => Shell.Current.GoToAsync(state, parameters);

    public Task GoToAsync(ShellNavigationState state, bool animate, IDictionary<string, object> parameters) => Shell.Current.GoToAsync(state, animate, parameters);
}
