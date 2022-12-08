using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IDialogService _dialogService;

    public MainViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, IDialogService dialogService)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _dialogService = dialogService;
    }

    [RelayCommand]
    private async Task OpenScanner()
    {
        await _navigation.GoToAsync($"{PageConstants.SCANNER_PAGE}?BackTargetPage={PageConstants.ADD_ARTICLE_PAGE}");
    }

    public ObservableRangeCollection<Grouping<string, ArticleModel>> ArticleGroups { get; set; } = new();

    public ObservableRangeCollection<ArticleModel> Articles { get; set; } = new();


    [RelayCommand]
    public async Task Init()
    {
        try
        {
            await Load();
        }
        catch (Exception ex)
        {
        }
    }

    [RelayCommand]
    public async Task PerformSearch(string query)
    {
        await Task.CompletedTask;
    }

    [RelayCommand]
    public async Task Delete(ArticleModel article)
    {
        if (article == null)
            return;

        await _pantryClientApiService.DeleteArticleAsync(article.Id);
        await Load();
    }

    [RelayCommand]
    public async Task Tap(ArticleModel article)
    {
        if (article == null)
            return;

        await _navigation.GoToAsync($"{PageConstants.ADD_ARTICLE_PAGE}?Id={article.Id}");
    }

    [RelayCommand]
    public async Task Refresh()
    {
        try
        {
            await Load();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task Load()
    {
        var articleListResponse = await _pantryClientApiService.GetAllArticlesAsync();
        var articles = (from item in articleListResponse?.Articles select item.ToArticleModel()).ToList();
        Articles.Clear();
        Articles.AddRange(articles);
        var groups = articles.GroupBy(x => x.StorageLocation?.Name);
        ArticleGroups.Clear();
        ArticleGroups.AddRange(from item in groups select new Grouping<string, ArticleModel>(item.Key, item));
    }


    [RelayCommand]
    public async Task Add()
    {
        await _navigation.GoToAsync($"{PageConstants.SCANNER_PAGE}?BackTargetPage={PageConstants.ADD_ARTICLE_PAGE}");
    }
}
