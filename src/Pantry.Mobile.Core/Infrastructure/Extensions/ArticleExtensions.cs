using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class ArticleExtensions
{
    public static ArticleRequest ToArticleRequest(this ArticleModel articleModel)
        => new()
        {
            BestBeforeDate = DateTime.SpecifyKind(articleModel.BestBeforeDate, DateTimeKind.Utc),
            Content = articleModel.Content,
            ContentType = articleModel.ContentType,
            GlobalTradeItemNumber = articleModel.GlobalTradeItemNumber,
            Name = articleModel.Name,
            Quantity = articleModel.Quantity,
            StorageLocationId = articleModel.StorageLocation?.Id ?? 0
        };

    public static ArticleModel ToArticleModel(this ArticleRequest articleRequest, StorageLocationModel storageLocationModel)
        => new()
        {
            BestBeforeDate = DateTime.SpecifyKind(articleRequest.BestBeforeDate, DateTimeKind.Utc),
            Content = articleRequest.Content,
            ContentType = articleRequest.ContentType,
            GlobalTradeItemNumber = articleRequest.GlobalTradeItemNumber,
            Name = articleRequest.Name,
            Quantity = articleRequest.Quantity,
            StorageLocation = storageLocationModel
        };

    public static ArticleModel ToArticleModel(this ArticleResponse articleResponse)
    => new()
    {
        BestBeforeDate = DateTime.SpecifyKind(articleResponse.BestBeforeDate, DateTimeKind.Utc),
        Content = articleResponse.Content,
        ContentType = articleResponse.ContentType,
        GlobalTradeItemNumber = articleResponse.GlobalTradeItemNumber,
        Id = articleResponse.Id,
        Name = articleResponse.Name,
        Quantity = articleResponse.Quantity,
        StorageLocation = articleResponse.StorageLocation.ToStorageLocationModel()
    };
}
