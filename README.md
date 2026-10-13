# Unit Converter API

A RESTful Web API built with **ASP.NET Core and C#** that converts numerical values between different measurement units, including length, temperature, and weight/mass.

## Features

- Convert values between supported measurement units.
- Support for length, temperature, and weight/mass conversions.
- Centralized unit registry for managing conversion factors.
- RESTful API endpoints with JSON request and response formats.
- Input validation and error handling.
- Swagger/OpenAPI integration for API documentation and testing.
- Extensible design to make adding more units easier.

## Technologies Used

- C#
- ASP.NET Core Web API
- .NET 8
- Swagger / OpenAPI
- Git and GitHub

## Project Structure

```text
UnitConvertorApi/
├── Controllers/
│   └── UnitConverterController.cs
├── Models/
│   ├── ConversionRequest.cs
│   ├── ConversionResponse.cs
│   └── UnitInfo.cs
├── Services/
│   ├── UnitConversionService.cs
│   └── UnitRegistry.cs
├── Program.cs
├── appsettings.json
├── .gitignore
└── README.md
```

## Supported Conversion Categories

### 1. Length

| Unit | Symbol |
|---|---|
| Meters | m |
| Feet | ft |
| Kilometers | km |
| Miles | mi |
| Centimeters | cm |
| Inches | in |
| Millimeters | mm |
| Yards | yd |

### 2. Temperature

| Unit | Symbol |
|---|---|
| Celsius | °C |
| Fahrenheit | °F |
| Kelvin | K |

### 3. Weight / Mass

| Unit | Symbol |
|---|---|
| Kilograms | kg |
| Grams | g |
| Pounds | lb |
| Ounces | oz |
| Milligrams | mg |
| Tonnes | t |

## API Endpoints

### 1. Convert a Value

**Endpoint:** `POST /api/UnitConverter/convert`

Converts a numerical value from one unit to another within the specified category.

**Example request: Meters to Feet**

```json
{
  "category": "length",
  "value": 10,
  "fromUnit": "meters",
  "toUnit": "feet"
}
```

**Example response:**

```json
{
  "category": "length",
  "inputValue": 10,
  "fromUnit": "meters",
  "toUnit": "feet",
  "result": 32.808398950
}
```

### 2. Get Supported Categories

**Endpoint:** `GET /api/UnitConverter/categories`

Returns the available conversion categories.

**Example response:**

```json
[
  "length",
  "temperature",
  "weight"
]
```

### 3. Get Units by Category

**Endpoint:** `GET /api/UnitConverter/units/{category}`

Returns the supported units and their symbols for the specified category.

**Example:** `GET /api/UnitConverter/units/length`

**Example response:**

```json
[
  {
    "name": "meters",
    "symbol": "m"
  },
  {
    "name": "feet",
    "symbol": "ft"
  }
]
```

The response includes all supported length units, not just the two shown above.

## Additional Request Examples

### Celsius to Fahrenheit

```json
{
  "category": "temperature",
  "value": 25,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

Expected result: `77`

### Kilograms to Pounds

```json
{
  "category": "weight",
  "value": 5,
  "fromUnit": "kilograms",
  "toUnit": "pounds"
}
```

Expected result: approximately `11.0231`

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or a compatible .NET development environment

### Run the Application

1. Clone the repository:

   ```bash
   git clone https://github.com/YOUR-USERNAME/unit-converter-api.git
   ```

2. Navigate to the project directory:

   ```bash
   cd unit-converter-api
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Open the Swagger URL displayed in the terminal, typically:

   `https://localhost:<port>/swagger`

   Replace `<port>` with the actual port assigned to your application.

## Error Handling

The API returns appropriate HTTP error responses for invalid requests.

| HTTP Status | Description |
|---|---|
| 200 OK | Conversion or retrieval successful |
| 400 Bad Request | Unsupported category, invalid unit, missing required input, or invalid temperature |

For example, an unsupported length unit such as `stones` returns a `400 Bad Request` response with an explanatory message.

Temperatures below absolute zero are rejected.

## Architecture and Design

The application separates responsibilities into three main layers:

- **Controllers:** Handle HTTP requests and return HTTP responses.
- **Models:** Define request, response, and unit information structures.
- **Services:** Perform conversion calculations and manage unit definitions.

The `UnitRegistry` stores conversion factors for length and weight units. These categories convert through a common base unit, while temperature uses dedicated formulas because its scales have different zero points.

This design makes the project easier to maintain and provides a foundation for adding more measurement units and categories in the future.

## Future Improvements

- Add automated unit and integration tests.
- Introduce additional measurement categories.
- Load unit definitions from configuration or a database.
- Add API versioning and structured logging.
- Add authentication if required by future use cases.

## Author

**Your Name**

GitHub: `https://github.com/YOUR-USERNAME`

## License

This project is available for educational and portfolio purposes. Add a license file if you intend to distribute it under specific terms.
