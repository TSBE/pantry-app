using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class AccountExtensions
{
    public static AccountModel ToAccountModel(this AccountResponse accountResponse)
    => new()
    {
        FirstName = accountResponse.FirstName,
        LastName = accountResponse.LastName,
        FriendsCode = accountResponse.FriendsCode,
        Household = accountResponse.Household?.ToHouseholdModel()
    };
}
