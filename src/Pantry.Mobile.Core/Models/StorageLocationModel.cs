using CommunityToolkit.Mvvm.ComponentModel;

namespace Pantry.Mobile.Core.Models;

public partial class StorageLocationModel : ObservableObject
{
    /// <summary>
    /// Represents the database internal id.
    /// </summary>
    [ObservableProperty]
    public long id;

    /// <summary>
    /// The name of the location.
    /// </summary>
    [ObservableProperty]
    public string name = string.Empty;

    /// <summary>
    /// The description.
    /// </summary>
    [ObservableProperty]
    public string description = string.Empty;
}
