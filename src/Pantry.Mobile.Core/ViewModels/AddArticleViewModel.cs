using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(Barcode), nameof(Barcode))]
public partial class AddArticleViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IKeyboardHelper _keyboardHelper;

    public AddArticleViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, IKeyboardHelper keyboardHelper)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _keyboardHelper = keyboardHelper;
    }

    [ObservableProperty] private long id;

    [ObservableProperty] private ArticleModel articleModel = new();

    [ObservableProperty] private int selectedStorageLocationIndex;

    [ObservableProperty] private string barcode = string.Empty;

    public ObservableRangeCollection<StorageLocationModel> StorageLocations { get; } = [];

    [RelayCommand]
    private async Task LoadStorageLocations()
    {
        try
        {
            IsBusy = true;
            var storageLocationList = await _pantryClientApiService.GetAllStorageLocationsAsync();
            var storageLocations = (from item in storageLocationList?.StorageLocations select item.ToStorageLocationModel()).ToList();
            StorageLocations.Clear();
            StorageLocations.AddRange(storageLocations);
        }
        catch (Exception)
        {
            // ignored
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task LoadMetadata()
    {
        try
        {
            IsBusy = true;
            var metadataResponse = await _pantryClientApiService.GetMetadataByGtinAsync(Barcode);
            ArticleModel.Name = metadataResponse.Name ?? string.Empty;
            ArticleModel.Content = metadataResponse.Brands ?? string.Empty;
            ArticleModel.ImageUrl = metadataResponse.ImageUrl ?? null;
        }
        catch (Exception)
        {
            // ignored
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task LoadArticle(long articleId)
    {
        try
        {
            IsBusy = true;
            var articleResponse = await _pantryClientApiService.GetArticleByIdAsync(articleId);
            ArticleModel = articleResponse.ToArticleModel();
        }
        catch (Exception)
        {
            // ignored
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task Save()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(ArticleModel.GlobalTradeItemNumber) ||
            string.IsNullOrWhiteSpace(ArticleModel.Name) ||
            ArticleModel.Quantity <= 0 ||
            ArticleModel.StorageLocation is null)
        {
            return;
        }

        IsBusy = true;
        try
        {
            var articleRequest = ArticleModel.ToArticleRequest();

            if (Id > 0)
            {
                await _pantryClientApiService.UpdateArticleAsync(articleRequest, Id);
            }
            else
            {
                await _pantryClientApiService.CreateArticleAsync(articleRequest);
            }
            await _navigation.GoToAsync("..");
            _keyboardHelper.HideKeyboard();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    async partial void OnIdChanged(long value)
    {
        await LoadStorageLocations();
        await LoadArticle(value);
        SelectStorageLocation();
    }

    async partial void OnBarcodeChanged(string value)
    {
        await LoadStorageLocations();
        await LoadMetadata();
        ArticleModel.GlobalTradeItemNumber = value;
        SelectStorageLocation();
    }

    private void SelectStorageLocation()
    {
        var location = StorageLocations.FirstOrDefault(x => x.Id == ArticleModel.StorageLocation?.Id);
        if (location is not null)
        {
            SelectedStorageLocationIndex = StorageLocations.IndexOf(location);
        }
        else
        {
            ArticleModel.StorageLocation = StorageLocations.FirstOrDefault();
        }
    }
}
