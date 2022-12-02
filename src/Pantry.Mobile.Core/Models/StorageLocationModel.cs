namespace Pantry.Mobile.Core.Models;

public class StorageLocationModel
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of the location.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
