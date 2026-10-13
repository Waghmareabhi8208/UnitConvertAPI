
using System.ComponentModel.DataAnnotations;

namespace UnitConvertorApi.Models;

public sealed class ConversionRequest
{
    [Required]
    public string Category { get; init; } = string.Empty;

    [Required]
    public decimal? Value { get; init; }

    [Required]
    public string FromUnit { get; init; } = string.Empty;

    [Required]
    public string ToUnit { get; init; } = string.Empty;
}
