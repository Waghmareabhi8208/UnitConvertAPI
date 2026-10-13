
using UnitConvertorApi.Models;

namespace UnitConvertorApi.Services;

public sealed class UnitRegistry
{
    public sealed record LinearUnit(
        string Name,
        string Symbol,
        decimal FactorToBase);

    private readonly Dictionary<string, Dictionary<string, LinearUnit>>
        _linearUnits =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["length"] = new(StringComparer.OrdinalIgnoreCase)
                {
                    ["meters"] = new("meters", "m", 1m),
                    ["feet"] = new("feet", "ft", 0.3048m),
                    ["kilometers"] = new("kilometers", "km", 1000m),
                    ["miles"] = new("miles", "mi", 1609.344m),
                    ["centimeters"] = new("centimeters", "cm", 0.01m),
                    ["inches"] = new("inches", "in", 0.0254m),
                    ["millimeters"] = new("millimeters", "mm", 0.001m),
                    ["yards"] = new("yards", "yd", 0.9144m)
                },

                ["weight"] = new(StringComparer.OrdinalIgnoreCase)
                {
                    ["kilograms"] = new("kilograms", "kg", 1m),
                    ["grams"] = new("grams", "g", 0.001m),
                    ["pounds"] = new("pounds", "lb", 0.45359237m),
                    ["ounces"] = new("ounces", "oz", 0.028349523125m),
                    ["milligrams"] = new("milligrams", "mg", 0.000001m),
                    ["tonnes"] = new("tonnes", "t", 1000m)
                }
            };

    private static readonly Dictionary<string, UnitInfo>
        TemperatureUnits =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["celsius"] = new("celsius", "°C"),
                ["fahrenheit"] = new("fahrenheit", "°F"),
                ["kelvin"] = new("kelvin", "K")
            };

    public IReadOnlyList<string> GetCategories()
    {
        return new[] { "length", "temperature", "weight" };
    }

    public IReadOnlyList<UnitInfo> GetUnits(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException(
                "Category is required.");
        }

        category = category.Trim();

        if (category.Equals(
            "temperature",
            StringComparison.OrdinalIgnoreCase))
        {
            return TemperatureUnits.Values.ToArray();
        }

        if (!_linearUnits.TryGetValue(category, out var units))
        {
            throw new ArgumentException(
                $"Unsupported category: {category}");
        }

        return units.Values
            .Select(unit => new UnitInfo(unit.Name, unit.Symbol))
            .ToArray();
    }

    public LinearUnit GetLinearUnit(
        string category,
        string unit)
    {
        if (!_linearUnits.TryGetValue(category, out var units))
        {
            throw new ArgumentException(
                $"Unsupported category: {category}");
        }

        if (!units.TryGetValue(unit, out var definition))
        {
            throw new ArgumentException(
                $"Unsupported {category} unit: {unit}");
        }

        return definition;
    }
}
