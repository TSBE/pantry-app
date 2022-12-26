namespace Pantry.Mobile.Core.Models;

public class NutrimentModel
{
    public string? Name { get; set; }

    public string? Unit { get; set; }

    public double Value { get; set; }

    public double ValueFor100g { get; set; }

    public string ValueFor100gWithUnit
    {
        get
        {
            return $"{ValueFor100g:0.##} {Unit}";
        }
    }
}
