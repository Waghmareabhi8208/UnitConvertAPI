
namespace UnitConvertorApi.Services
{
    public class UnitConversionService
    {
        public decimal Convert(
            string category,
            decimal value,
            string fromUnit,
            string toUnit)
        {
            category = category.Trim().ToLowerInvariant();
            fromUnit = fromUnit.Trim().ToLowerInvariant();
            toUnit = toUnit.Trim().ToLowerInvariant();

            return category switch
            {
                "length" => ConvertLength(value, fromUnit, toUnit),
                "temperature" => ConvertTemperature(value, fromUnit, toUnit),
                "weight" => ConvertWeight(value, fromUnit, toUnit),

                _ => throw new ArgumentException(
                    "Unsupported category. Use length, temperature, or weight.")
            };
        }

        private decimal ConvertLength(
            decimal value, string from, string to)
        {
            decimal meters = from switch
            {
                "meters" => value,
                "feet" => value / 3.280839895m,
                "kilometers" => value * 1000m,
                "miles" => value * 1609.344m,
                "centimeters" => value / 100m,
                "inches" => value * 0.0254m,
                _ => throw new ArgumentException(
                    $"Unsupported length unit: {from}")
            };

            return to switch
            {
                "meters" => meters,
                "feet" => meters * 3.280839895m,
                "kilometers" => meters / 1000m,
                "miles" => meters / 1609.344m,
                "centimeters" => meters * 100m,
                "inches" => meters / 0.0254m,
                _ => throw new ArgumentException(
                    $"Unsupported length unit: {to}")
            };
        }

        private decimal ConvertWeight(
            decimal value, string from, string to)
        {
            decimal kilograms = from switch
            {
                "kilograms" => value,
                "grams" => value / 1000m,
                "pounds" => value / 2.2046226218m,
                "ounces" => value / 35.27396195m,
                _ => throw new ArgumentException(
                    $"Unsupported weight unit: {from}")
            };

            return to switch
            {
                "kilograms" => kilograms,
                "grams" => kilograms * 1000m,
                "pounds" => kilograms * 2.2046226218m,
                "ounces" => kilograms * 35.27396195m,
                _ => throw new ArgumentException(
                    $"Unsupported weight unit: {to}")
            };
        }

        private decimal ConvertTemperature(
            decimal value, string from, string to)
        {
            decimal celsius = from switch
            {
                "celsius" => value,
                "fahrenheit" => (value - 32m) * 5m / 9m,
                "kelvin" => value - 273.15m,
                _ => throw new ArgumentException(
                    $"Unsupported temperature unit: {from}")
            };

            if (celsius < -273.15m)
            {
                throw new ArgumentException(
                    "Temperature cannot be below absolute zero.");
            }

            return to switch
            {
                "celsius" => celsius,
                "fahrenheit" => celsius * 9m / 5m + 32m,
                "kelvin" => celsius + 273.15m,
                _ => throw new ArgumentException(
                    $"Unsupported temperature unit: {to}")
            };
        }
    }
}
