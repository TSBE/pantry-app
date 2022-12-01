#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents an account that holds user informations.
/// </summary>
public class AccountResponse
{
    /// <summary>
    /// The users first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The users last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// A guid which is the public invate id.
    /// </summary>
    public Guid FriendsCode { get; set; }
}
