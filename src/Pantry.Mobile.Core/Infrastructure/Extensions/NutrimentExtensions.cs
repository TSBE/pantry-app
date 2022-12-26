using Pantry.Mobile.Core.Infrastructure.Helpers;
using Pantry.Mobile.Core.Infrastructure.Services.PantryService.Models;
using Pantry.Mobile.Core.Models;

namespace Pantry.Mobile.Core.Infrastructure.Extensions;

public static class NutrimentExtensions
{
    public static NutrimentModel ToNutrimentModel(this NutrimentResponse nutrimentResponse, string name)
    => new()
    {
        Name = GetName(name),
        Unit = nutrimentResponse.Unit,
        Value = nutrimentResponse.Value,
        ValueFor100g = nutrimentResponse.ValueFor100g
    };

    public static ObservableRangeCollection<NutrimentModel>? ToNutrimentModels(this IDictionary<string, NutrimentResponse> nutrimentResponses)
    {

        var nutriments = new ObservableRangeCollection<NutrimentModel>();
        foreach (var nutriment in nutrimentResponses)
        {
            if (ShouldSkip(nutriment.Key))
            {
                continue;
            }
            nutriments.Add(nutriment.Value.ToNutrimentModel(nutriment.Key));
        }

        return nutriments;
    }

    private static string GetName(ReadOnlySpan<char> key) =>
       key switch
       {
           "SaturadedFat" => "Saturaded fat",
           _ => key.ToString(),
       };

    private static bool ShouldSkip(ReadOnlySpan<char> key) =>
       key switch
       {
           "Energykcal" => true,
           _ => false,
       };
}
