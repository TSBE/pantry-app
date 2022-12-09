using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class InvitationExtensions
{
    public static InvitationModel ToInvitationModel(this InvitationResponse invitationResponse)
    => new()
    {
        ValidUntilDate = DateTime.SpecifyKind(invitationResponse.ValidUntilDate, DateTimeKind.Utc),
        CreatorName = invitationResponse.CreatorName,
        FriendsCode = invitationResponse.FriendsCode,
        HouseholdName = invitationResponse.HouseholdName
    };
}
