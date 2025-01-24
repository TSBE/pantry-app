using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class MainViewModelFixture
{
    [Fact]
    public void AddCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        // Act
        vm.AddCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.SCANNER_PAGE}?BackTargetPage={PageConstants.ADD_ARTICLE_PAGE}"));
    }

    [Fact]
    public void DeleteCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);
        var model = new ArticleModel { Id = 1 };

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(new ArticleListResponse()));

        // Act
        vm.DeleteCommand.Execute(model);

        // Assert
        pantryClient.Received(1).DeleteArticleAsync(Arg.Is(model.Id));
        pantryClient.Received(1).GetAllArticlesAsync();
    }

    [Fact]
    public void InitCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(new ArticleListResponse()));

        // Act
        vm.InitCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllArticlesAsync();
    }

    [Fact]
    public void LoadCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(new ArticleListResponse()));

        // Act
        vm.LoadCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllArticlesAsync();
    }

    [Fact]
    public void PerformSearchCommand_ShoudWork()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(
            new ArticleListResponse
            {
                Articles = new List<ArticleResponse>
                {
                    new ArticleResponse { Name = "Unit", StorageLocation = new StorageLocationResponse { Name = "Dummy" } },
                    new ArticleResponse { Name = "Test", StorageLocation = new StorageLocationResponse { Name = "Dummy" } }
                }
            }));

        // Act
        vm.LoadCommand.Execute(null);

        // Act
        vm.PerformSearchCommand.Execute("Test");

        // Assert
        pantryClient.Received(1).GetAllArticlesAsync();
        vm.FilteredArticles.Count.ShouldBe(1);
        vm.FilteredArticles.FirstOrDefault().ShouldNotBeNull();
        vm.FilteredArticles.FirstOrDefault()!.Name.ShouldBe("Test");
    }

    [Fact]
    public void RefreshCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(new ArticleListResponse()));

        // Act
        vm.RefreshCommand.Execute(null);

        // Assert
        pantryClient.Received(1).GetAllArticlesAsync();
    }

    [Fact]
    public void TapCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var vm = new MainViewModel(navigation, pantryClient);

        var model = new ArticleModel { Id = 1 };

        // Act
        vm.TapCommand.Execute(model);

        // Assert        
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.ARTICLE_DETAIL_PAGE}?Id={model.Id}"));
    }
}
