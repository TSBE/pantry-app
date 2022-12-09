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
        return Task.FromResult(new ArticleResponse
        {
            Name = articleRequest.Name,
            BestBeforeDate = articleRequest.BestBeforeDate,
            StorageLocation = new StorageLocationResponse
            {
                Name = "Dummy Location"
            }
        });
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
        return Task.FromResult(new InvitationResponse
        {
            CreatorName = "Jane Doe",
            HouseholdName = "Jane's Household",
            ValidUntilDate = DateTime.UtcNow.AddDays(10),
            FriendsCode = invitationRequest.FriendsCode
        });
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
            FirstName = "Jane",
            LastName = "Doe",
            FriendsCode = Guid.NewGuid(),
            Household = new HouseholdResponse
            {
                Name = "Jane's Household",
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
                    Id = 1,
                    Name = "Milk",
                    BestBeforeDate = DateTime.UtcNow.Date.AddDays(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Kitchen cabinet"
                    }
                },
                new ArticleResponse
                {
                    Id = 2,
                    Name = "Rice",
                    BestBeforeDate = DateTime.UtcNow.Date.AddMonths(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Kitchen cabinet"
                    }
                },
                new ArticleResponse
                {
                    Id = 3,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
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
                   Id = 1,
                   Name = "Küche",
                   Description="Dummy Description"
               },
                new StorageLocationResponse
               {
                    Id = 2,
                   Name = "Keller",
                   Description="Dummy Description"
               }
            }
        });
        //return Task.FromResult<StorageLocationListResponse>(null);
    }

    public Task<ArticleResponse> GetArticleByIdAsync(long articleId)
    {
        return Task.FromResult(
            new ArticleResponse
            {
                Id = articleId,
                GlobalTradeItemNumber = "5745000121045",
                Name = $"Dummy Article {articleId}",
                BestBeforeDate = DateTime.UtcNow,
                Quantity = 1,
                StorageLocation = new StorageLocationResponse
                {
                    Id = 2,
                    Name = "Dummy Location 2",
                    Description = "Dummy Description"
                }
            });
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
                   CreatorName = "Jane Doe",
                   FriendsCode = Guid.NewGuid(),
                   HouseholdName ="Jane's Household",
                   ValidUntilDate = DateTime.UtcNow.AddDays(1)
               },
               new InvitationResponse
               {
                   CreatorName = "Dummy Creator",
                   FriendsCode = Guid.NewGuid(),
                   HouseholdName ="Dummy Household",
                   ValidUntilDate = DateTime.UtcNow.AddDays(10)
               }
            }
        });
    }

    public Task<MetadataResponse> GetMetadataByGtinAsync(string barcode)
    {
        return Task.FromResult(new MetadataResponse
        {
            Name = "Zitrone",
            GlobalTradeItemNumber = "5745000121045",
            Brands = "True Gum",
            ImageUrl = "https://images.openfoodfacts.org/images/products/574/500/012/1045/front_de.3.400.jpg"
        });
    }

    public Task<StorageLocationResponse> GetStorageLocationByIdAsync(long storageLocationId)
    {
        throw new NotImplementedException();
    }

    public Task<ArticleResponse> UpdateArticleAsync([Body] ArticleRequest articleRequest, long articleId)
    {
        return Task.FromResult(new ArticleResponse
        {
            Id = articleId,
            Name = articleRequest.Name,
            BestBeforeDate = articleRequest.BestBeforeDate,
            StorageLocation = new StorageLocationResponse
            {
                Name = "Dummy Location"
            }
        });
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
