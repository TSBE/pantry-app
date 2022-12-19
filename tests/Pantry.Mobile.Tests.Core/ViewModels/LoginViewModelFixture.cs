using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class LoginViewModelFixture
{
    [Fact]
    public void LoginCommand_ShoudCallLogin()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var auth0Client = Substitute.For<IAuth0Client>();
        var vm = new LoginViewModel(navigation, settingsService, auth0Client);

        // Act
        vm.LoginCommand.Execute(null);

        // Assert
        auth0Client.Received(1).Login();
    }
}
