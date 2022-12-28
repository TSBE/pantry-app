using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ArticleDetailViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    public ArticleDetailViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
    }

    [ObservableProperty]
    public long id;

    [ObservableProperty]
    public ArticleModel article = new();


    [RelayCommand]
    public async Task LoadArticle(long id)
    {
        try
        {
            IsBusy = true;
            var articleResponse = await _pantryClientApiService.GetArticleByIdAsync(id);
            Article = articleResponse.ToArticleModel();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    public async Task Back()
    {
        try
        {
            IsBusy = true;
            await _navigation.GoToAsync("..");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally { IsBusy = false; }
    }

    partial void OnIdChanged(long value)
    {
        LoadArticleCommand?.Execute(value);
    }
}
