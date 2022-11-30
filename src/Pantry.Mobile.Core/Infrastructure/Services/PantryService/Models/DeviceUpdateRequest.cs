#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// Represents a users device.
/// </summary>
public class DeviceUpdateRequest
{
    /// <summary>
    /// A device name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The device token for push notifications.
    /// </summary>
    public string? DeviceToken { get; set; }
}
