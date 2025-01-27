using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

namespace Pantry.Mobile.Core.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(Name), nameof(Name))]
[QueryProperty(nameof(Description), nameof(Description))]
public partial class AddStorageLocationViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    private readonly IKeyboardHelper? _keyboardHelper;

    public AddStorageLocationViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService, IKeyboardHelper keyboardHelper)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
        _keyboardHelper = keyboardHelper;
    }

    public long Id { get; set; }

    [ObservableProperty] private string name = string.Empty;

    [ObservableProperty] private string description = string.Empty;

    [RelayCommand]
    private async Task Save()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Description))
        {
            return;
        }

        IsBusy = true;
        try
        {
            if (Id > 0)
            {
                await _pantryClientApiService.UpdateStorageLocationAsync(new StorageLocationRequest { Name = Name, Description = Description }, Id);
            }
            else
            {
                await _pantryClientApiService.CreateStorageLocationAsync(new StorageLocationRequest { Name = Name, Description = Description });
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
}
