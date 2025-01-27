using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class SettingsViewModelFixture
{
    [Fact]
    public void InitCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var settingsService = Substitute.For<ISettingsService>();
        var auth0Client = Substitute.For<IAuth0Client>();
        var vm = new SettingsViewModel(navigation, pantryClient, settingsService, auth0Client);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAccountAsync();
    }

    [Fact]
    public void LogoutCommand_ShoudCallLogout()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var settingsService = Substitute.For<ISettingsService>();
        var auth0Client = Substitute.For<IAuth0Client>();
        var vm = new SettingsViewModel(navigation, pantryClient, settingsService, auth0Client);

        navigation.GetNextStartupPage(Arg.Any<CancellationToken>()).Returns(Task.FromResult("unittest"));

        // Act
        vm.LogoutCommand.Execute(null);

        // Assert
        auth0Client.Received(1).Logout();
        settingsService.Received(1).DeleteCredentials();
        navigation.Received(1).GetNextStartupPage(Arg.Any<CancellationToken>());
        navigation.Received(1).GoToAsync(Arg.Is("unittest"));
    }

    [Fact]
    public void ManageInvitationsCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var settingsService = Substitute.For<ISettingsService>();
        var auth0Client = Substitute.For<IAuth0Client>();
        var vm = new SettingsViewModel(navigation, pantryClient, settingsService, auth0Client);

        // Act
        vm.ManageInvitationsCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.ManageInvitationsPage}"));
    }
}
