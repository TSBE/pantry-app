using System.ComponentModel.DataAnnotations;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Refit;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService;

[Headers("Accept: application/json; charset=utf-8",
"Accept-Encoding: gzip, deflate",
"cache-control: no-cache")]
public interface IPantryClientApiService
{
    /// <summary>
    /// Get my account.
    /// </summary>
    /// <returns>returns logged in users account.</returns>
    [Get("/accounts/me")]
    public Task<AccountResponse> GetAccountAsync();

    /// <summary>
    /// Creates the accoount from the logged in user if not yet done so and returns the account.
    /// </summary>
    /// <returns>account.</returns>
    [Put("/accounts/me")]
    public Task<AccountResponse> CreateAccountAsync([Body] AccountRequest accountRequest);

    /// <summary>
    ///  Deletes the account for the logged in user.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Delete("/accounts/me")]
    public Task<HttpResponseMessage> DeleteAccountAsync();

    /// <summary>
    /// Gets all devices.
    /// </summary>
    /// <returns>List of all devices.</returns>
    [Get("/devices")]
    public Task<DeviceListResponse> GetAllDevicesAsync();

    /// <summary>
    /// Gets device by installationId.
    /// </summary>
    /// <returns>List of all devices.</returns>
    [Get("/devices/{installationId}")]
    public Task<DeviceResponse> GetDeviceByIdAsync(Guid installationId);

    /// <summary>
    /// Create device.
    /// </summary>
    /// <returns>device.</returns>
    [Post("/devices")]
    public Task<DeviceResponse> CreateDeviceAsync([Body] DeviceRequest deviceRequest);

    /// <summary>
    /// Update device.
    /// </summary>
    /// <returns>device.</returns>
    [Put("/devices/{installationId}")]
    public Task<DeviceResponse> UpdateDeviceAsync([Body] DeviceUpdateRequest deviceUpdateRequest, Guid installationId);

    /// <summary>
    /// Delete device.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Delete("/devices/{installationId}")]
    public Task<HttpResponseMessage> DeleteDeviceAsync(Guid installationId);

    /// <summary>
    /// Get my household.
    /// </summary>
    /// <returns>returns logged in users household.</returns>
    [Get("/households/my")]
    public Task<HouseholdResponse> GetHouseholdAsync();

    /// <summary>
    /// Creates household from the logged in user if not yet done so and returns the household.
    /// </summary>
    /// <returns>household.</returns>
    [Post("/households/my")]
    public Task<HouseholdResponse> CreateHouseholdAsync([Body] HouseholdRequest householdRequest);

    /// <summary>
    /// Gets the invitations for the logged in user.
    /// </summary>
    /// <returns>returns invitation.</returns>
    [Get("/invitations/my")]
    public Task<InvitationListResponse> GetInvitationAsync(CancellationToken? ct = null);

    /// <summary>
    /// Creates a new invitation for the logged in user and returns the newly created model.
    /// </summary>
    /// <returns>invitation.</returns>
    [Post("/invitations")]
    public Task<InvitationResponse> CreateInvitationAsync([Body] InvitationRequest invitationRequest);

    /// <summary>
    /// Accept the Invitation.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Post("/invitations/{friendsCode}/accept")]
    public Task AcceptInvitationAsync([Url] Guid friendsCode);

    /// <summary>
    /// Decline the Invitation.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Post("/invitations/{friendsCode}/decline")]
    public Task DeclineInvitationAsync([Url] Guid friendsCode);

    /// <summary>
    /// Get storage location.
    /// </summary>
    /// <returns>returns logged in users storage location.</returns>
    [Get("/storage-locations/{storageLocationId}")]
    public Task<StorageLocationResponse> GetStorageLocationByIdAsync(long storageLocationId);

    /// <summary>
    /// Gets all storageLocations.
    /// </summary>
    /// <returns>List of all storageLocations.</returns>
    [Get("/storage-locations")]
    public Task<StorageLocationListResponse> GetAllStorageLocationsAsync();

    /// <summary>
    /// Creates storage location.
    /// </summary>
    /// <returns>storage location.</returns>
    [Post("/storage-locations")]
    public Task<StorageLocationResponse> CreateStorageLocationAsync([Body] StorageLocationRequest storageLocationRequest);

    /// <summary>
    /// Update storage location.
    /// </summary>
    /// <returns>storage location.</returns>
    [Put("/storage-locations/{storageLocationId}")]
    public Task<StorageLocationResponse> UpdateStorageLocationAsync([Body] StorageLocationRequest storageLocationRequest, long storageLocationId);

    /// <summary>
    ///  Deletes storage location.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Delete("/storage-locations/{storageLocationId}")]
    public Task<HttpResponseMessage> DeleteStorageLocationAsync(long storageLocationId);

    /// <summary>
    /// Get article.
    /// </summary>
    /// <returns>returns logged in users article.</returns>
    [Get("/articles/{articleId}")]
    public Task<ArticleResponse> GetArticleByIdAsync(long articleId);

    /// <summary>
    /// Get articles.
    /// </summary>
    /// <returns>List of articles.</returns>
    [Get("/articles")]
    public Task<ArticleListResponse> GetAllArticlesAsync();

    /// <summary>
    /// Creates article.
    /// </summary>
    /// <returns>article.</returns>
    [Post("/articles")]
    public Task<ArticleResponse> CreateArticleAsync([Body] ArticleRequest articleRequest);

    /// <summary>
    /// Update article.
    /// </summary>
    /// <returns>article.</returns>
    [Put("/articles/{articleId}")]
    public Task<ArticleResponse> UpdateArticleAsync([Body] ArticleRequest articleRequest, long articleId);

    /// <summary>
    ///  Deletes article.
    /// </summary>
    /// <returns>no content.</returns>
    [Headers("Content-Type: application/json; charset=UTF-8")]
    [Delete("/articles/{articleId}")]
    public Task<HttpResponseMessage> DeleteArticleAsync(long articleId);

    /// <summary>
    ///  Get metadata.
    /// </summary>
    /// <returns>metadata.</returns>
    [Get("/metadatas/{barcode}")]
    public Task<MetadataResponse> GetMetadataByGtinAsync(string barcode);
}
