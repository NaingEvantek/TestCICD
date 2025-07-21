using System.Net;
using System.Net.Http.Json;
using Xunit;
using MySimpleApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MySimpleApi.Tests
{
    public class WeatherForecastApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public WeatherForecastApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsFiveItems()
        {
            // Act
            var response = await _client.GetAsync("/weatherforecast");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
            Assert.NotNull(result);
            Assert.Equal(5, result.Length);
        }
    }

    // This class must match your WeatherForecast model
    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);
}
