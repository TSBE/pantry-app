using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class HouseholdExtensions
{
    public static HouseholdModel ToHouseholdModel(this HouseholdResponse householdResponse)
    => new()
    {
        Name = householdResponse.Name,
        SubscriptionType = householdResponse.SubscriptionType
    };
}
