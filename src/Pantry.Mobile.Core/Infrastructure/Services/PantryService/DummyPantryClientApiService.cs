using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Refit;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService;

public class DummyPantryClientApiService : IPantryClientApiService
{
    public Task AcceptInvitationAsync([Url] Guid friendsCode)
    {
        return Task.CompletedTask;
    }

    public Task<AccountResponse> CreateAccountAsync([Body] AccountRequest accountRequest)
    {
        return Task.FromResult(new AccountResponse
        {
            FirstName = accountRequest.FirstName,
            LastName = accountRequest.LastName,
            FriendsCode = Guid.NewGuid()
        });
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
        return Task.FromResult(
            new DeviceResponse
            {
                DeviceToken = deviceRequest.DeviceToken,
                InstallationId = deviceRequest.InstallationId,
                Model = deviceRequest.Model,
                Platform = deviceRequest.Platform,
                Name = deviceRequest.Name
            });
    }

    public Task<HouseholdResponse> CreateHouseholdAsync([Body] HouseholdRequest householdRequest)
    {
        return Task.FromResult(
            new HouseholdResponse
            {
                Name = householdRequest.Name,
                SubscriptionType = Enums.SubscriptionType.FREE
            });
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
        return Task.CompletedTask;
    }

    public Task<HttpResponseMessage> DeleteAccountAsync()
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
    }

    public Task<HttpResponseMessage> DeleteArticleAsync(long articleId)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
    }

    public Task<HttpResponseMessage> DeleteDeviceAsync(Guid installationId)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
    }

    public Task<HttpResponseMessage> DeleteStorageLocationAsync(long storageLocationId)
    {
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent));
    }

    public Task<AccountResponse> GetAccountAsync()
    {
        return Task.FromResult(
            new AccountResponse
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
                    ImageUrl = "https://images.openfoodfacts.org/images/products/574/500/012/1045/front_de.3.400.jpg",
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
                    ImageUrl = "https://images.openfoodfacts.org/images/products/574/500/012/1045/front_de.3.400.jpg",
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
                    ImageUrl = "https://images.openfoodfacts.org/images/products/574/500/012/1045/front_de.3.400.jpg",
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 4,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 5,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 6,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 7,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 8,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 9,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 10,
                    Name = "Honey",
                    BestBeforeDate = DateTime.UtcNow.Date.AddYears(1),
                    StorageLocation = new StorageLocationResponse
                    {
                        Name = "Basement"
                    }
                },
                new ArticleResponse
                {
                    Id = 11,
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
        return Task.FromResult(new DeviceListResponse
        {
            Devices = new List<DeviceResponse>
            {
                new DeviceResponse
                {
                    DeviceToken = null,
                    InstallationId = Guid.NewGuid(),
                    Model = "iPhone 12",
                    Platform = Enums.DevicePlatformType.IOS
                },
                new DeviceResponse
                {
                    DeviceToken = null,
                    InstallationId = Guid.NewGuid(),
                    Model = "Samsung Galaxy S22",
                    Platform = Enums.DevicePlatformType.ANDROID
                }
            }
        });
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
                ImageUrl = "https://images.openfoodfacts.org/images/products/574/500/012/1045/front_de.3.400.jpg",
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
        return Task.FromResult(new DeviceResponse
        {
            DeviceToken = null,
            InstallationId = installationId,
            Model = "iPhone 12",
            Platform = Enums.DevicePlatformType.IOS
        });
    }

    public Task<HouseholdResponse> GetHouseholdAsync()
    {
        return Task.FromResult(
            new HouseholdResponse
            {
                Name = "Dummy Household",
                SubscriptionType = Enums.SubscriptionType.FREE
            });
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
        return Task.FromResult(
            new StorageLocationResponse
            {
                Id = 1,
                Name = "Dummy Location",
                Description = "Dummy Description"
            });
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
        return Task.FromResult(new DeviceResponse
        {
            DeviceToken = null,
            InstallationId = installationId,
            Model = "iPhone 12",
            Platform = Enums.DevicePlatformType.IOS
        });
    }

    public Task<StorageLocationResponse> UpdateStorageLocationAsync([Body] StorageLocationRequest storageLocationRequest, long storageLocationId)
    {
        return Task.FromResult(new StorageLocationResponse { Id = storageLocationId, Name = storageLocationRequest.Name, Description = storageLocationRequest.Description });
    }
}
