using CommunityToolkit.Mvvm.ComponentModel;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Enums;

namespace Pantry.Mobile.Core.Models;

[INotifyPropertyChanged]
public partial class HouseholdModel
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
