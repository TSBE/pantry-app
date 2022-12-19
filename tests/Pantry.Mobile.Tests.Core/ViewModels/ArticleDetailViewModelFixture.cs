using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class ArticleDetailViewModelFixture
{
    [Fact]
    public void SaveCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ArticleDetailViewModel(navigation, pantryClient);

        // Act
        vm.LoadArticleCommand.Execute(1);

        // Assert
        pantryClient.Received(1).GetArticleByIdAsync(Arg.Is(1L));
    }

    [Fact]
    public void BackCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new ArticleDetailViewModel(navigation, pantryClient);

        // Act
        vm.BackCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is(".."));
    }
}
