using CommunityToolkit.Mvvm.ComponentModel;

namespace Pantry.Mobile.Core.Models;

public partial class AccountModel : ObservableObject
{
    /// <summary>
    /// The users first name.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    public string? firstName;

    /// <summary>
    /// The users last name.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    public string? lastName;

    /// <summary>
    /// A guid which is the public invate id.
    /// </summary>
    [ObservableProperty]
    public Guid friendsCode;

    /// <summary>
    /// Represents a household.
    /// </summary>
    [ObservableProperty]
    public HouseholdModel? household;

    public string Initials
    {
        get
        {
            var initials = FirstName?[..1] ?? string.Empty;
            if (!string.IsNullOrEmpty(LastName))
            {
                initials += LastName?[..(initials.Length == 1 ? 1 : 2)] ?? "?";
            }
            else
            {
                initials = LastName?[..2] ?? "?";
            }
            return initials;
        }
    }
}
