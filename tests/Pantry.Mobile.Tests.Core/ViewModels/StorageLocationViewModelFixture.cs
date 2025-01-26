using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class StorageLocationViewModelFixture
{
    [Fact]
    public void AddCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        // Act
        vm.AddCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.AddStorageLocationPage}"));
    }

    [Fact]
    public void DeleteCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        var model = new StorageLocationModel { Id = 1 };

        // Act
        vm.DeleteCommand.Execute(model);

        // Assert
        pantryClient.Received(1).DeleteStorageLocationAsync(Arg.Is(model.Id));
        pantryClient.Received(1).GetAllStorageLocationsAsync();
    }

    [Fact]
    public void InitCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllStorageLocationsAsync();
    }

    [Fact]
    public void LoadCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        // Act
        vm.LoadCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllStorageLocationsAsync();
    }

    [Fact]
    public void RefreshCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        // Act
        vm.RefreshCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllStorageLocationsAsync();
    }
    [Fact]
    public void TapCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new StorageLocationViewModel(navigation, pantryClient);

        var model = new StorageLocationModel { Id = 1, Name = "Unit", Description = "Test" };

        // Act
        vm.TapCommand.Execute(model);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.AddStorageLocationPage}?Id={model.Id}&Name={model.Name}&Description={model.Description}"));
    }
}
