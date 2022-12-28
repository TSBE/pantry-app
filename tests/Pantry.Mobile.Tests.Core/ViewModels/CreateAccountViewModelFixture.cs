using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class CreateAccountViewModelFixture
{
    [Fact]
    public void SaveCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new CreateAccountViewModel(navigation, pantryClient)
        {
            FirstName = "Jane",
            LastName = "Doe"
        };

        // Act
        vm.CreateAccountCommand.Execute(null);

        // Assert
        pantryClient.Received(1).CreateAccountAsync(Arg.Any<AccountRequest>());
    }
}
