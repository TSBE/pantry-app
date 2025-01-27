using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;
using Pantry.Mobile.Core.ViewModels;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Tests.Core.ViewModels;

public class MainViewModelFixture
{
    [Fact]
    public void AddCommand_ShoudCallNavigation()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

        // Act
        vm.AddCommand.Execute(null);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.ScannerPage}?BackTargetPage={PageConstants.AddArticlePage}&ActiveBarcodeFormat={BarcodeFormat.Ean13}"));
    }

    [Fact]
    public void DeleteCommand_ShoudCallApi()
    {
        // Arrange
        var navigation = Substitute.For<INavigationService>();
        var pantryClient = Substitute.For<IPantryClientApiService>();
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);
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
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

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
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

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
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

        pantryClient.GetAllArticlesAsync().Returns(Task.FromResult(
            new ArticleListResponse
            {
                Articles = new List<ArticleResponse>
                {
                    new() { Name = "Unit", StorageLocation = new StorageLocationResponse { Name = "Dummy" } },
                    new() { Name = "Test", StorageLocation = new StorageLocationResponse { Name = "Dummy" } }
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
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

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
        var keyboardHelper = Substitute.For<IKeyboardHelper>();

        var vm = new MainViewModel(navigation, pantryClient, keyboardHelper);

        var model = new ArticleModel { Id = 1 };

        // Act
        vm.TapCommand.Execute(model);

        // Assert
        navigation.Received(1).GoToAsync(Arg.Is($"{PageConstants.ArticleDetailPage}?Id={model.Id}"));
    }
}
