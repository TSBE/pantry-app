#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents a storage location for articles.
/// </summary>
public class StorageLocationResponse
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of the location.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description.
    /// </summary>
    public string Description { get; set; }
}
