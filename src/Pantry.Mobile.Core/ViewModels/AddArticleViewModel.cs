using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(Name), nameof(Name))]
[QueryProperty(nameof(Description), nameof(Description))]
[QueryProperty(nameof(Barcode), nameof(Barcode))]
public partial class AddArticleViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IKeyboardHelper? _keyboardHelper;

    public AddArticleViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, IKeyboardHelper keyboardHelper)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _keyboardHelper = keyboardHelper;
    }

    public long Id { get; set; }

    [ObservableProperty]
    public string name = string.Empty;

    [ObservableProperty]
    public string description = string.Empty;

    [ObservableProperty]
    public string barcode = string.Empty;

    [RelayCommand]
    public async Task Init()
    {
        try
        {
            IsBusy = true;
            await LoadMetadata();
        }
        catch (Exception ex)
        {
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    public async Task Save()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(description))
        {
            return;
        }

        IsBusy = true;
        try
        {
            if (Id > 0)
            {
                await _pantryClientApiService.UpdateArticleAsync(new ArticleRequest { Name = Name }, Id);
            }
            else
            {
                await _pantryClientApiService.CreateArticleAsync(new ArticleRequest { Name = Name });
            }
            await _navigation.GoToAsync("..");
            _keyboardHelper?.HideKeyboard();
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

    private async Task LoadMetadata()
    {
        var metadataResponse = await _pantryClientApiService.GetMetadataByGtinAsync(Barcode);
        Name = metadataResponse.Name;
        Description = metadataResponse.Brands;
    }
}
