
using Microsoft.AspNetCore.Mvc;
using UnitConvertorApi.Models;
using UnitConvertorApi.Services;

namespace UnitConvertorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UnitConverterController : ControllerBase
{
    private readonly UnitConversionService _conversionService;
    private readonly UnitRegistry _registry;

    public UnitConverterController(
        UnitConversionService conversionService,
        UnitRegistry registry)
    {
        _conversionService = conversionService;
        _registry = registry;
    }

    [HttpPost("convert")]
    [ProducesResponseType(
        typeof(ConversionResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ConversionResponse> Convert(
        [FromBody] ConversionRequest request)
    {
        if (request.Value is null)
        {
            return BadRequest(new
            {
                message = "Value is required."
            });
        }

        if (string.IsNullOrWhiteSpace(request.Category) ||
            string.IsNullOrWhiteSpace(request.FromUnit) ||
            string.IsNullOrWhiteSpace(request.ToUnit))
        {
            return BadRequest(new
            {
                message =
                    "Category, FromUnit and ToUnit are required."
            });
        }

        try
        {
            decimal result = _conversionService.Convert(
                request.Category,
                request.Value.Value,
                request.FromUnit,
                request.ToUnit);

            return Ok(new ConversionResponse(
                request.Category.Trim().ToLowerInvariant(),
                request.Value.Value,
                request.FromUnit.Trim(),
                request.ToUnit.Trim(),
                result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (OverflowException)
        {
            return BadRequest(new
            {
                message =
                    "The conversion result is outside the supported numeric range."
            });
        }
    }

    [HttpGet("categories")]
    public ActionResult<IEnumerable<string>> GetCategories()
    {
        return Ok(_registry.GetCategories());
    }

    [HttpGet("units/{category}")]
    public IActionResult GetUnits(string category)
    {
        try
        {
            return Ok(_registry.GetUnits(category));
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
