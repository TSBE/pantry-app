using Pantry.Mobile.Core.Infrastructure.Abstractions;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService;

public sealed class MockedPantryClientApiServiceDecorator(
    ISettingsService settingsService,
    IPantryClientApiService real,
    IPantryClientApiService mock)
    : IPantryClientApiService
{
    private IPantryClientApiService Service { get => settingsService.IsMockModeEnabled ? mock : real; }

    public Task<AccountResponse> GetAccountAsync()
    {
        return Service.GetAccountAsync();
    }

    public Task<AccountResponse> CreateAccountAsync(AccountRequest accountRequest)
    {
        return Service.CreateAccountAsync(accountRequest);
    }

    public Task<HttpResponseMessage> DeleteAccountAsync()
    {
        return Service.DeleteAccountAsync();
    }

    public Task<DeviceListResponse> GetAllDevicesAsync()
    {
        return Service.GetAllDevicesAsync();
    }

    public Task<DeviceResponse> GetDeviceByIdAsync(Guid installationId)
    {
        return Service.GetDeviceByIdAsync(installationId);
    }

    public Task<DeviceResponse> CreateDeviceAsync(DeviceRequest deviceRequest)
    {
        return Service.CreateDeviceAsync(deviceRequest);
    }

    public Task<DeviceResponse> UpdateDeviceAsync(DeviceUpdateRequest deviceUpdateRequest, Guid installationId)
    {
        return Service.UpdateDeviceAsync(deviceUpdateRequest, installationId);
    }

    public Task<HttpResponseMessage> DeleteDeviceAsync(Guid installationId)
    {
        return Service.DeleteDeviceAsync(installationId);
    }

    public Task<HouseholdResponse> GetHouseholdAsync()
    {
        return Service.GetHouseholdAsync();
    }

    public Task<HouseholdResponse> CreateHouseholdAsync(HouseholdRequest householdRequest)
    {
        return Service.CreateHouseholdAsync(householdRequest);
    }

    public Task<InvitationListResponse> GetInvitationAsync(CancellationToken? ct = null)
    {
        return Service.GetInvitationAsync(ct);
    }

    public Task<InvitationResponse> CreateInvitationAsync(InvitationRequest invitationRequest)
    {
        return Service.CreateInvitationAsync(invitationRequest);
    }

    public Task AcceptInvitationAsync(Guid friendsCode)
    {
        return Service.AcceptInvitationAsync(friendsCode);
    }

    public Task DeclineInvitationAsync(Guid friendsCode)
    {
        return Service.DeclineInvitationAsync(friendsCode);
    }

    public Task<StorageLocationResponse> GetStorageLocationByIdAsync(long storageLocationId)
    {
        return Service.GetStorageLocationByIdAsync(storageLocationId);
    }

    public Task<StorageLocationListResponse> GetAllStorageLocationsAsync()
    {
        return Service.GetAllStorageLocationsAsync();
    }

    public Task<StorageLocationResponse> CreateStorageLocationAsync(StorageLocationRequest storageLocationRequest)
    {
        return Service.CreateStorageLocationAsync(storageLocationRequest);
    }

    public Task<StorageLocationResponse> UpdateStorageLocationAsync(StorageLocationRequest storageLocationRequest, long storageLocationId)
    {
        return Service.UpdateStorageLocationAsync(storageLocationRequest, storageLocationId);
    }

    public Task<HttpResponseMessage> DeleteStorageLocationAsync(long storageLocationId)
    {
        return Service.DeleteStorageLocationAsync(storageLocationId);
    }

    public Task<ArticleResponse> GetArticleByIdAsync(long articleId)
    {
        return Service.GetArticleByIdAsync(articleId);
    }

    public Task<ArticleListResponse> GetAllArticlesAsync()
    {
        return Service.GetAllArticlesAsync();
    }

    public Task<ArticleResponse> CreateArticleAsync(ArticleRequest articleRequest)
    {
        return Service.CreateArticleAsync(articleRequest);
    }

    public Task<ArticleResponse> UpdateArticleAsync(ArticleRequest articleRequest, long articleId)
    {
        return Service.UpdateArticleAsync(articleRequest, articleId);
    }

    public Task<HttpResponseMessage> DeleteArticleAsync(long articleId)
    {
        return Service.DeleteArticleAsync(articleId);
    }

    public Task<MetadataResponse> GetMetadataByGtinAsync(string barcode)
    {
        return Service.GetMetadataByGtinAsync(barcode);
    }
}
