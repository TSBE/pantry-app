using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class StorageLocationExtensions
{
    public static StorageLocationRequest ToStorageLocationRequest(this StorageLocationModel storageLocationModel)
        => new()
        {
            Description = storageLocationModel.Description,
            Name = storageLocationModel.Name
        };

    public static StorageLocationModel ToStorageLocationModel(this StorageLocationRequest storageLocationRequest)
        => new()
        {
            Description = storageLocationRequest.Description,
            Name = storageLocationRequest.Name
        };

    public static StorageLocationModel ToStorageLocationModel(this StorageLocationResponse storageLocationResponse)
        => new()
        {
            Description = storageLocationResponse.Description,
            Id = storageLocationResponse.Id,
            Name = storageLocationResponse.Name
        };
}
