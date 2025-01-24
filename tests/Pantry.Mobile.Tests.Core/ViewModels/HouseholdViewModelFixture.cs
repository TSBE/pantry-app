using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class HouseholdViewModelFixture
{
    [Fact]
    public void AcceptInvitationCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient);

        var model = new InvitationModel { FriendsCode = Guid.NewGuid() };

        // Act
        vm.AcceptInvitationCommand.Execute(model);

        // Assert
        pantryClient.Received(1).AcceptInvitationAsync(Arg.Is(model.FriendsCode));
        pantryClient.DidNotReceive().DeclineInvitationAsync(Arg.Any<Guid>());
    }

    [Fact]
    public void DeclineInvitationCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient);

        var model = new InvitationModel { FriendsCode = Guid.NewGuid() };

        // Act
        vm.DeclineInvitationCommand.Execute(model);

        // Assert
        pantryClient.Received(1).DeclineInvitationAsync(Arg.Is(model.FriendsCode));
        pantryClient.DidNotReceive().AcceptInvitationAsync(Arg.Any<Guid>());
    }

    [Fact]
    public void CreateHouseholdCommandCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient) { Name = "Test" };
        navigation.GetNextStartupPage(Arg.Any<CancellationToken>()).Returns(Task.FromResult("unittest"));

        // Act
        vm.CreateHouseholdCommand.Execute(null);

        // Assert
        pantryClient.Received(1).CreateHouseholdAsync(Arg.Any<HouseholdRequest>());
        navigation.Received(1).GetNextStartupPage(Arg.Any<CancellationToken>());
        navigation.Received(1).GoToAsync(Arg.Is("unittest"), Arg.Is(false));
    }

    [Fact]
    public void InitCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAccountAsync();
    }

    [Fact]
    public void ToggleCreateCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient);

        // Act
        vm.ToggleCreateCommand.Execute(null);

        // Assert
        vm.IsCreateVisible.ShouldBeTrue();
        vm.IsJoinVisible.ShouldBeFalse();
    }

    [Fact]
    public void ToggleJoinCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new HouseholdViewModel(navigation, pantryClient);

        // Act
        vm.ToggleJoinCommand.Execute(null);

        // Assert
        vm.IsJoinVisible.ShouldBeTrue();
        vm.IsCreateVisible.ShouldBeFalse();
    }
}
