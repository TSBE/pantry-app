using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class AddArticleViewModelFixture
{
    [Fact]
    public void LoadArticleCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();
        var vm = new AddArticleViewModel(navigation, pantryClient, keyboardHelper);

        // Act
        vm.LoadArticleCommand.Execute(1);

        // Assert
        pantryClient.Received(1).GetArticleByIdAsync(Arg.Any<long>());
    }

    [Fact]
    public void LoadMetadataCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();
        var vm = new AddArticleViewModel(navigation, pantryClient, keyboardHelper);

        // Act
        vm.LoadMetadataCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetMetadataByGtinAsync(Arg.Any<string>());
    }

    [Fact]
    public void LoadStorageLocationsCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();
        var vm = new AddArticleViewModel(navigation, pantryClient, keyboardHelper);

        // Act
        vm.LoadStorageLocationsCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllStorageLocationsAsync();
    }

    [Fact]
    public void SaveCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();
        var vm = new AddArticleViewModel(navigation, pantryClient, keyboardHelper)
        {
            ArticleModel = new ArticleModel { BestBeforeDate = DateTime.UtcNow, GlobalTradeItemNumber = "test", Name = "UnitTest", Quantity = 42, StorageLocation = new StorageLocationModel() }
        };

        // Act
        vm.SaveCommand.Execute(null);

        // Assert
        pantryClient.Received(1).CreateArticleAsync(Arg.Any<ArticleRequest>());
    }
}
