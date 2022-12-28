#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;

namespace Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;

/// <summary>
/// <inheritdoc />
/// </summary>
public class DeviceRequest : DeviceUpdateRequest
{
    /// <summary>
    /// The device model.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Information about the operating system.
    /// </summary>
    public DevicePlatformType Platform { get; set; }

    /// <summary>
    /// Installation identifier.
    /// </summary>
    public Guid InstallationId { get; set; }
}
