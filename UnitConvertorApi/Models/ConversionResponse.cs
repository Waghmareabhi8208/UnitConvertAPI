
namespace UnitConvertorApi.Models;

public sealed record ConversionResponse(
    string Category,
    decimal InputValue,
    string FromUnit,
    string ToUnit,
    decimal Result);
