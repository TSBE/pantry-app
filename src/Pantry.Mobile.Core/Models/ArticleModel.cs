using CommunityToolkit.Mvvm.ComponentModel;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

namespace Pantry.Mobile.Core.Models;

[INotifyPropertyChanged]
public partial class ArticleModel
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    [ObservableProperty]
    public long id;

    /// <summary>
    /// Storage location id.
    /// </summary>
    [ObservableProperty]
    public StorageLocationModel? storageLocation;

    /// <summary>
    /// The Global Trade Item Number (GTIN) a.k.a. (EAN) of the article.
    /// </summary>
    [ObservableProperty]
    public string? globalTradeItemNumber;

    /// <summary>
    /// The name of the article.
    /// </summary>
    [ObservableProperty]
    public string name = string.Empty;

    /// <summary>
    /// The best before date.
    /// </summary>
    [ObservableProperty]
    public DateTime bestBeforeDate = DateTime.UtcNow.Date;

    /// <summary>
    /// The quantity article.
    /// </summary>
    [ObservableProperty]
    public int quantity = 1;

    /// <summary>
    /// The content of the article.
    /// </summary>
    [ObservableProperty]
    public string? content;

    /// <summary>
    /// The content type of the article.
    /// </summary>
    [ObservableProperty]
    public ContentType contentType = ContentType.UNKNOWN;

    /// <summary>
    /// The brands.
    /// </summary>
    [ObservableProperty]
    public string? brands;

    /// <summary>
    /// The image url.
    /// </summary>
    [ObservableProperty]
    public string? imageUrl;

    /// <summary>
    /// Nutriments.
    /// </summary>
    public IDictionary<string, NutrimentResponse>? Nutriments { get; set; }
}
