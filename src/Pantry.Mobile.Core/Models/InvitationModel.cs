namespace Pantry.Mobile.Core.Models;

public class InvitationModel
{
    /// <summary>
    /// A guid which is the public invate id.
    /// </summary>
    public Guid FriendsCode { get; set; }

    /// <summary>
    /// The creator of the invitation.
    /// </summary>
    public string CreatorName { get; set; } = string.Empty;

    /// <summary>
    /// Invitation valid until.
    /// </summary>
    public DateTime ValidUntilDate { get; set; }

    /// <summary>
    /// The household to which this invitation belongs.
    /// </summary>
    public string HouseholdName { get; set; } = string.Empty;
}
