using System.ComponentModel.DataAnnotations;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Refit;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService;

public class DummyPantryClientApiService : IPantryClientApiService
{
    public Task AcceptInvitationAsync([Url] Guid friendsCode)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse> CreateAccountAsync([Body] AccountRequest accountRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleResponse> CreateArticleAsync([Body] ArticleRequest articleRequest)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceResponse> CreateDeviceAsync([Body] DeviceRequest deviceRequest)
    {
        throw new NotImplementedException();
    }

    public Task<HouseholdResponse> CreateHouseholdAsync([Body] HouseholdRequest householdRequest)
    {
        throw new NotImplementedException();
    }

    public Task<InvitationResponse> CreateInvitationAsync([Body] InvitationRequest invitationRequest)
    {
        throw new NotImplementedException();
    }

    public Task<StorageLocationResponse> CreateStorageLocationAsync([Body] StorageLocationRequest storageLocationRequest)
    {
        return Task.FromResult(new StorageLocationResponse { Id = 1, Name = storageLocationRequest.Name, Description = storageLocationRequest.Description });
    }

    public Task DeclineInvitationAsync([Url] Guid friendsCode)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteAccountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteArticleAsync(long articleId)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceResponse> DeleteDeviceAsync(Guid installationId)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> DeleteStorageLocationAsync(long storageLocationId)
    {
        throw new NotImplementedException();
    }

    public Task<AccountResponse> GetAccountAsync()
    {
        return Task.FromResult(new AccountResponse
        {
            FirstName = "Dummy",
            LastName = "Dummy",
            FriendsCode = Guid.NewGuid(),
            Household = new HouseholdResponse
            {
                Name = "Dummy Household",
                SubscriptionType = Enums.SubscriptionType.FREE
            }
        });
    }

    public Task<ArticleListResponse> GetAllArticlesAsync()
    {
        return Task.FromResult(new ArticleListResponse
        {
            Articles = new List<ArticleResponse>
            {
                new ArticleResponse
                {
                    Name = "Dummy Article",
                    BestBeforeDate = DateTime.UtcNow,
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Dummy Location"
                    }
                }
            }
        });
        //return Task.FromResult<ArticleListResponse>(null);
    }

    public Task<DeviceListResponse> GetAllDevicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<StorageLocationListResponse> GetAllStorageLocationsAsync()
    {
        return Task.FromResult(new StorageLocationListResponse
        {
            StorageLocations = new List<StorageLocationResponse>
            {
               new StorageLocationResponse
               {
                   Name = "Dummy Location",
                   Description="Dummy Description"
               }
            }
        });
        //return Task.FromResult<StorageLocationListResponse>(null);
    }

    public Task<ArticleResponse> GetArticleByIdAsync(long articleId)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceResponse> GetDeviceByIdAsync(Guid installationId)
    {
        throw new NotImplementedException();
    }

    public Task<HouseholdResponse> GetHouseholdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InvitationListResponse> GetInvitationAsync(CancellationToken? ct = null)
    {
        return Task.FromResult(new InvitationListResponse
        {
            Invitations = new List<InvitationResponse>
            {
               new InvitationResponse
               {
                   CreatorName = "Dummy Creator",
                   FriendsCode = Guid.NewGuid(),
                   HouseholdName ="Dummy Household",
                   ValidUntilDate = DateTime.UtcNow.AddDays(1)
               }
            }
        });
    }

    public Task<StorageLocationResponse> GetStorageLocationByIdAsync(long storageLocationId)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleResponse> UpdateArticleAsync([Body] ArticleRequest articleRequest, long articleId)
    {
        throw new NotImplementedException();
    }

    public Task<DeviceResponse> UpdateDeviceAsync([Body] DeviceUpdateRequest deviceUpdateRequest, Guid installationId)
    {
        throw new NotImplementedException();
    }

    public Task<StorageLocationResponse> UpdateStorageLocationAsync([Body] StorageLocationRequest storageLocationRequest, long storageLocationId)
    {
        return Task.FromResult(new StorageLocationResponse { Id = storageLocationId, Name = storageLocationRequest.Name, Description = storageLocationRequest.Description });
    }
}
