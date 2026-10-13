
using System.ComponentModel.DataAnnotations;

namespace UnitConvertorApi.Models
{
    public class ConversionRequest
    {
        [Required]
        public string Category { get; set; } = string.Empty;

        public decimal Value { get; set; }

        [Required]
        public string FromUnit { get; set; } = string.Empty;

        [Required]
        public string ToUnit { get; set; } = string.Empty;
    }
}
