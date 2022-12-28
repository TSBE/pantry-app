#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents article.
/// </summary>
public class ArticleResponse
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Storage location.
    /// </summary>
    public StorageLocationResponse StorageLocation { get; set; }

    /// <summary>
    /// The Global Trade Item Number (GTIN) a.k.a. (EAN) of the article.
    /// </summary>
    public string? GlobalTradeItemNumber { get; set; }

    /// <summary>
    /// The name of the article.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The best before date.
    /// </summary>
    public DateTime BestBeforeDate { get; set; }

    /// <summary>
    /// The quantity article.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The content of the article.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// The content type of the article.
    /// </summary>
    public ContentType ContentType { get; set; }

    /// <summary>
    /// The brands.
    /// </summary>
    public string? Brands { get; set; }

    /// <summary>
    /// The image url.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Nutriments.
    /// </summary>
    public IDictionary<string, NutrimentResponse>? Nutriments { get; set; }
}
