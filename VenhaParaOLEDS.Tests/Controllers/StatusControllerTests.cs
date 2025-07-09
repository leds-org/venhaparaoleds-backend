using Xunit;
using Microsoft.AspNetCore.Mvc;
using VenhaParaOLEDS.Controllers;
using System;
using System.Text.Json;

namespace VenhaParaOLEDS.Tests.Controllers
{
    public class StatusControllerTests
    {
        [Fact]
        public void Health_DeveRetornarHealthy()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            var result = controller.Health();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Healthy", okResult.Value);
        }

        [Fact]
        public void GetStatus_DeveRetornarInformacoesDaAplicacao()
        {
            // Arrange
            var controller = new StatusController();

            // Act
            var result = controller.GetStatus();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var json = JsonSerializer.Serialize(okResult.Value);
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

            Assert.Equal("VenhaParaOLEDS API", jsonElement.GetProperty("App").GetString());
            Assert.False(string.IsNullOrEmpty(jsonElement.GetProperty("Version").GetString()));
            Assert.True(DateTime.TryParse(jsonElement.GetProperty("Timestamp").GetString(), out _));
            Assert.False(string.IsNullOrEmpty(jsonElement.GetProperty("Environment").GetString()));
            Assert.Equal("Ok", jsonElement.GetProperty("Database").GetString());
        }
    }
}