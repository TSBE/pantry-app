#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents an Invitation to a household.
/// </summary>
public class InvitationResponse
{
    /// <summary>
    /// A guid which is the public invate id.
    /// </summary>
    public Guid FriendsCode { get; set; }

    /// <summary>
    /// The creator of the invitation.
    /// </summary>
    public string CreatorName { get; set; }

    /// <summary>
    /// Invitation valid until.
    /// </summary>
    public DateTime ValidUntilDate { get; set; }

    /// <summary>
    /// The household to which this invitation belongs.
    /// </summary>
    public string HouseholdName { get; set; }
}
