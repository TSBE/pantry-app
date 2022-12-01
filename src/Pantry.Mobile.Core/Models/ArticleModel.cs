namespace Pantry.Mobile.Core.Models;

public class ArticleModel
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Storage location id.
    /// </summary>
    public long StorageLocationId { get; set; }

    /// <summary>
    /// The Global Trade Item Number (GTIN) a.k.a. (EAN) of the article.
    /// </summary>
    public string? GlobalTradeItemNumber { get; set; }

    /// <summary>
    /// The name of the article.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The best before date.
    /// </summary>
    public DateTime BestBeforeDate { get; set; }

    public string Image { get; set; } = string.Empty;
}
