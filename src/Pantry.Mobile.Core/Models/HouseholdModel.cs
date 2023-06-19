using CommunityToolkit.Mvvm.ComponentModel;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;

namespace Pantry.Mobile.Core.Models;

public partial class HouseholdModel : ObservableObject
{
    /// <summary>
    /// The name of the household.
    /// </summary>
    [ObservableProperty]
    public string? name;

    /// <summary>
    /// The Subscription type.
    /// </summary>
    [ObservableProperty]
    public SubscriptionType subscriptionType;
}
