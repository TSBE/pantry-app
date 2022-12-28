using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class AddStorageLocationViewModelFixture
{
    [Fact]
    public void SaveCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();
        var vm = new AddStorageLocationViewModel(navigation, pantryClient, keyboardHelper)
        {
            Name = "UnitTest",
            Description = "Dummy"
        };

        // Act
        vm.SaveCommand.Execute(null);

        // Assert
        pantryClient.Received(1).CreateStorageLocationAsync(Arg.Any<StorageLocationRequest>());
    }
}
