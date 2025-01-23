using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Application.UseCases;
using Infrastructure.Repositories;
using Xunit;

namespace Tests
{
    public class EndpointServiceTests
    {
        [Fact]
        public void AddEndpoint_ShouldAddEndpointSuccessfully()
        {
            // Arrange
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);
            var endpoint = new Endpoint
            {
                EndpointSerialNumber = "12345",
                MeterModelId = 16,
                MeterNumber = 100,
                MeterFirmwareVersion = "v1.0",
                SwitchState = 0
            };

            // Act
            service.AddEndpoint(endpoint);

            // Assert
            var addedEndpoint = repository.GetBySerialNumber("12345");
            Assert.NotNull(addedEndpoint);
            Assert.Equal("12345", addedEndpoint.EndpointSerialNumber);
        }

        [Fact]
        public void AddEndpoint_ShouldThrowException_ForInvalidMeterModelId()
        {
            // Arrange
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);
            var invalidMeterModelId = "INVALID";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.ConvertMeterModelId(invalidMeterModelId));
            Assert.Equal("Invalid Meter Model ID. Allowed values are: NSX1P2W, NSX1P3W, NSX2P3W, NSX3P4W.", exception.Message);
        }

        [Fact]
        public void EditEndpoint_ShouldUpdateSwitchStateSuccessfully()
        {
            // Arrange
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);
            var endpoint = new Endpoint
            {
                EndpointSerialNumber = "12345",
                MeterModelId = 16,
                MeterNumber = 100,
                MeterFirmwareVersion = "v1.0",
                SwitchState = 0
            };
            repository.Add(endpoint);

            // Act
            service.EditEndpoint("12345", 1);

            // Assert
            var updatedEndpoint = repository.GetBySerialNumber("12345");
            Assert.NotNull(updatedEndpoint);
            Assert.Equal(1, updatedEndpoint.SwitchState);
        }

        [Fact]
        public void FindEndpoint_ShouldReturnCorrectEndpoint()
        {
            // Arrange
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);
            var endpoint = new Endpoint
            {
                EndpointSerialNumber = "12345",
                MeterModelId = 16,
                MeterNumber = 100,
                MeterFirmwareVersion = "v1.0",
                SwitchState = 0
            };
            repository.Add(endpoint);

            // Act
            var foundEndpoint = service.FindEndpoint("12345");

            // Assert
            Assert.NotNull(foundEndpoint);
            Assert.Equal("12345", foundEndpoint.EndpointSerialNumber);
        }

        [Fact]
        public void IntegrationTest_AddListDeleteEndpoint_ShouldWorkCorrectly()
        {
            // Arrange
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);

            var endpoint = new Endpoint
            {
                EndpointSerialNumber = "67890",
                MeterModelId = 17,
                MeterNumber = 200,
                MeterFirmwareVersion = "v2.0",
                SwitchState = 1
            };

            // Act
            service.AddEndpoint(endpoint);
            var allEndpoints = service.ListEndpoints();
            service.DeleteEndpoint("67890");

            // Assert
            Assert.Single(allEndpoints);
            Assert.Equal("67890", allEndpoints.First().EndpointSerialNumber);

            var deletedEndpoint = repository.GetBySerialNumber("67890");
            Assert.Null(deletedEndpoint);
        }
    }
}
