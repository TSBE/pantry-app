using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;
using ZXing.Net.Maui;

namespace Pantry.Mobile.Core.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IKeyboardHelper _keyboardHelper;

    public MainViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, IKeyboardHelper keyboardHelper)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _keyboardHelper = keyboardHelper;
    }

    private List<ArticleModel> Articles { get; set; } = [];

    public ObservableRangeCollection<Grouping<string, ArticleModel>> FilteredArticleGroups { get; set; } = [];

    public ObservableRangeCollection<ArticleModel> FilteredArticles { get; set; } = [];

    [ObservableProperty] private DateTime? filterByDate;

    [RelayCommand]
    private async Task Init()
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
    private Task PerformSearch(string query)
    {
        var filteredArticles = Articles.Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (filteredArticles.Count != 0)
        {
            SetFilteredList(filteredArticles);
        }

        _keyboardHelper.HideKeyboard();
        
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Delete(ArticleModel article)
    {
        await _pantryClientApiService.DeleteArticleAsync(article.Id);
        await Load();
    }

    [RelayCommand]
    private async Task Tap(ArticleModel article)
    {
        await _navigation.GoToAsync($"{PageConstants.ArticleDetailPage}?Id={article.Id}");
    }

    [RelayCommand]
    private async Task Refresh()
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
    private async Task Load()
    {
        FilterByDate = null;
        var articleListResponse = await _pantryClientApiService.GetAllArticlesAsync();

        if (articleListResponse?.Articles is null)
        {
            return;
        }

        Articles = (from item in articleListResponse.Articles select item.ToArticleModel()).ToList();
        SetFilteredList(Articles);
    }

    [RelayCommand]
    private async Task Add()
    {
        await _navigation.GoToAsync($"{PageConstants.ScannerPage}?BackTargetPage={PageConstants.AddArticlePage}&ActiveBarcodeFormat={BarcodeFormat.Ean13}");
    }

    [RelayCommand]
    private async Task Edit(ArticleModel article)
    {
        await _navigation.GoToAsync($"{PageConstants.AddArticlePage}?Id={article.Id}");
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
