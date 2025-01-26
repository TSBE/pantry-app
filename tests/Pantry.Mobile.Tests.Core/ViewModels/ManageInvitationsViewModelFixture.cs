using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;
using Pantry.Mobile.Core.Infrastructure;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class ManageInvitationsViewModelFixture
{
    [Fact]
    public void AddCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        // Act
        vm.AddCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.ScannerPage}?ActiveBarcodeFormat={BarcodeFormat.QrCode}"));
    }

    [Fact]
    public void CreateInvitationCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        var guid = Guid.NewGuid();

        // Act
        vm.CreateInvitationCommand.Execute(guid.ToString());

        // Assert
        pantryClient.Received(1).CreateInvitationAsync(Arg.Any<InvitationRequest>());
        pantryClient.Received(1).GetInvitationAsync();
    }

    [Fact]
    public void DeleteCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        var model = new InvitationModel { FriendsCode = Guid.NewGuid() };

        // Act
        vm.DeleteCommand.Execute(model);

        // Assert
        pantryClient.Received(1).DeclineInvitationAsync(Arg.Is(model.FriendsCode));
        pantryClient.Received(1).GetInvitationAsync();
    }

    [Fact]
    public void InitCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetInvitationAsync();
    }

    [Fact]
    public void LoadCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        // Act
        vm.LoadCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetInvitationAsync();
    }

    [Fact]
    public void RefreshCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ManageInvitationsViewModel(navigation, pantryClient);

        // Act
        vm.RefreshCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetInvitationAsync();
    }
}
