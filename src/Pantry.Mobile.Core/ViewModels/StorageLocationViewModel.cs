using CommunityToolkit.Mvvm.Input;
using Pantry.Mobile.Core.Infrastructure;
using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Extensions;
using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.ViewModels;

public partial class StorageLocationViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;

    private readonly IPantryClientApiService _pantryClientApiService;

    public StorageLocationViewModel(INavigationService navigation, IPantryClientApiService pantryClientApiService)
    {
        _navigation = navigation;
        _pantryClientApiService = pantryClientApiService;
    }

    public ObservableRangeCollection<StorageLocationModel> StorageLocations { get; set; } = [];

    [RelayCommand]
    private async Task Init()
    {
        try
        {
            await Load();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    [RelayCommand]
    private async Task Delete(StorageLocationModel locationModel)
    {
        await _pantryClientApiService.DeleteStorageLocationAsync(locationModel.Id);
        await Load();
    }

    [RelayCommand]
    private async Task Tap(StorageLocationModel locationModel)
    {
        await _navigation.GoToAsync($"{PageConstants.AddStorageLocationPage}?Id={locationModel.Id}&Name={locationModel.Name}&Description={locationModel.Description}");
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
        var storageLocationList = await _pantryClientApiService.GetAllStorageLocationsAsync();
        if (storageLocationList?.StorageLocations is null)
        {
            return;
        }

        var storageLocations = (from item in storageLocationList?.StorageLocations select item.ToStorageLocationModel()).ToList();
        StorageLocations.Clear();
        StorageLocations.AddRange(storageLocations);
    }

    [RelayCommand]
    private async Task Add()
    {
        await _navigation.GoToAsync($"{PageConstants.AddStorageLocationPage}");
    }
}
