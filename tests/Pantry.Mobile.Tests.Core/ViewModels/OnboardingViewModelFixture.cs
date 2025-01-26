using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class OnboardingViewModelFixture
{
    [Fact]
    public void InitCommand_ShoudCallSettingsService()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var vm = new OnboardingViewModel(navigation, settingsService);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        settingsService.Received(1).GetOnboardingHasBeenFinished();
    }

    [Fact]
    public void InitCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var vm = new OnboardingViewModel(navigation, settingsService);

        settingsService.GetOnboardingHasBeenFinished().Returns(true);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        settingsService.Received(1).GetOnboardingHasBeenFinished();
        navigation.Received(1).GoToAsync(Arg.Is($"//{PageConstants.LoginPage}"), Arg.Is(false));
    }

    [Fact]
    public void NextCommand_ShoudCount()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var vm = new OnboardingViewModel(navigation, settingsService);

        // Act
        vm.NextCommand.Execute(null);

        // Assert
        vm.Position.ShouldBe(1);
        navigation.DidNotReceive().GoToAsync(Arg.Any<string>(), Arg.Any<bool>());
    }

    [Fact]
    public void NextCommand_ShoudCallSettingsService()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var vm = new OnboardingViewModel(navigation, settingsService);
        vm.Position = vm.IntroScreens.Count - 1;

        // Act
        vm.NextCommand.Execute(null);

        // Assert
        settingsService.Received(1).SetOnboardingHasBeenFinished(Arg.Is(true));
        navigation.Received(1).GoToAsync(Arg.Is($"//{PageConstants.LoginPage}"), Arg.Is(false));
    }

    [Fact]
    public void PositionChangedCommand_ShoudSetButtonText()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var settingsService = Substitute.For<ISettingsService>();
        var vm = new OnboardingViewModel(navigation, settingsService);

        // Act
        vm.PositionChangedCommand.Execute(vm.IntroScreens.Count - 1);

        // Assert
        vm.ButtonText.ShouldBe("Start");
    }
}
