
namespace UnitConvertorApi.Services;

public sealed class UnitConversionService
{
    private readonly UnitRegistry _registry;

    public UnitConversionService(UnitRegistry registry)
    {
        _registry = registry;
    }

    public decimal Convert(
        string category,
        decimal value,
        string fromUnit,
        string toUnit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        ArgumentException.ThrowIfNullOrWhiteSpace(fromUnit);
        ArgumentException.ThrowIfNullOrWhiteSpace(toUnit);

        category = category.Trim();
        fromUnit = fromUnit.Trim();
        toUnit = toUnit.Trim();

        if (category.Equals(
            "temperature",
            StringComparison.OrdinalIgnoreCase))
        {
            return ConvertTemperature(value, fromUnit, toUnit);
        }

        var source = _registry.GetLinearUnit(category, fromUnit);
        var target = _registry.GetLinearUnit(category, toUnit);

        decimal baseValue = value * source.FactorToBase;

        return baseValue / target.FactorToBase;
    }

    private decimal ConvertTemperature(
        decimal value,
        string from,
        string to)
    {
        decimal celsius = from.ToLowerInvariant() switch
        {
            "celsius" or "centigrade" => value,

            "fahrenheit" =>
                (value - 32m) * 5m / 9m,

            "kelvin" => value - 273.15m,

            _ => throw new ArgumentException(
                $"Unsupported temperature unit: {from}")
        };

        if (celsius < -273.15m)
        {
            throw new ArgumentException(
                "Temperature cannot be below absolute zero.");
        }

        return to.ToLowerInvariant() switch
        {
            "celsius" or "centigrade" => celsius,

            "fahrenheit" =>
                celsius * 9m / 5m + 32m,

            "kelvin" => celsius + 273.15m,

            _ => throw new ArgumentException(
                $"Unsupported temperature unit: {to}")
        };
    }
}
