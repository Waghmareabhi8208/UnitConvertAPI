
using Microsoft.AspNetCore.Mvc;
using UnitConvertorApi.Models;
using UnitConvertorApi.Services;

namespace UnitConvertorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitConverterController : ControllerBase
    {
        private readonly UnitConversionService _conversionService;

        public UnitConverterController(
            UnitConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        [HttpPost("convert")]
        public IActionResult Convert(
            [FromBody] ConversionRequest request)
        {
            try
            {
                decimal result = _conversionService.Convert(
                    request.Category,
                    request.Value,
                    request.FromUnit,
                    request.ToUnit);

                return Ok(new
                {
                    category = request.Category,
                    inputValue = request.Value,
                    fromUnit = request.FromUnit,
                    toUnit = request.ToUnit,
                    result = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
