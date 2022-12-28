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

    public MainViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
    }

    private List<ArticleModel> Articles { get; set; } = new();

    public ObservableRangeCollection<Grouping<string, ArticleModel>> FilteredArticleGroups { get; set; } = new();

    public ObservableRangeCollection<ArticleModel> FilteredArticles { get; set; } = new();

    [ObservableProperty]
    public DateTime? filterByDate = null;

    [RelayCommand]
    public async Task Init()
    {
        try
        {
            await Load();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    [RelayCommand]
    public Task PerformSearch(string query)
    {
        var filteredArticles = Articles.Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (filteredArticles.Any())
        {
            SetFilteredList(filteredArticles);
        }

        return Task.CompletedTask;
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

        await _navigation.GoToAsync($"{PageConstants.ARTICLE_DETAIL_PAGE}?Id={article.Id}");
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
        FilterByDate = null;
        var articleListResponse = await _pantryClientApiService.GetAllArticlesAsync();

        if (articleListResponse?.Articles is null)
        {
            return;
        }

        Articles = (from item in articleListResponse?.Articles select item.ToArticleModel()).ToList();
        SetFilteredList(Articles);
    }

    [RelayCommand]
    public async Task Add()
    {
        await _navigation.GoToAsync($"{PageConstants.SCANNER_PAGE}?BackTargetPage={PageConstants.ADD_ARTICLE_PAGE}");
    }

    [RelayCommand]
    public async Task Edit(ArticleModel article)
    {
        if (article is null)
        {
            return;
        }

        await _navigation.GoToAsync($"{PageConstants.ADD_ARTICLE_PAGE}?Id={article.Id}");
    }

    private void SetFilteredList(IList<ArticleModel> filteredArticles)
    {
        FilteredArticles.Clear();
        FilteredArticles.AddRange(filteredArticles);
        var groups = FilteredArticles.GroupBy(x => x.StorageLocation?.Name);
        FilteredArticleGroups.Clear();
        FilteredArticleGroups.AddRange(from item in groups select new Grouping<string, ArticleModel>(item.Key, item));
    }

    partial void OnFilterByDateChanged(DateTime? value)
    {
        if (value is null)
        {
            return;
        }

        var filteredArticles = Articles.Where(x => x.BestBeforeDate <= value).ToList();

        if (filteredArticles.Any())
        {
            SetFilteredList(filteredArticles);
        }
    }
}
