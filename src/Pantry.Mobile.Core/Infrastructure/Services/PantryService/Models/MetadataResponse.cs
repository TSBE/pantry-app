using System.Collections.Generic;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents article metadata.
/// </summary>
public class MetadataResponse
{
    /// <summary>
    /// The Global Trade Item Number (GTIN) a.k.a. (EAN).
    /// </summary>
    public string? GlobalTradeItemNumber { get; set; }

    /// <summary>
    /// The name.
    /// </summary>
    public string? Name { get; set; }

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
