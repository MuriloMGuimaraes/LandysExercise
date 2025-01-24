using System;
using System.Collections.Generic;
using System.Linq;
using EndpointManagement.Application.UseCases;
using EndpointManagement.Domain.Entities;
using EndpointManagement.Infrastructure.Repositories;
using Xunit;

namespace Tests
{
    public class EndpointServiceTests
    {
        [Fact]
        public void AddEndpoint_WhenParametersAreCorrect_ShouldAddEndpointSuccessfully()
        {
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

            service.AddEndpoint(endpoint);

            var addedEndpoint = repository.GetBySerialNumber("12345");
            Assert.NotNull(addedEndpoint);
            Assert.Equal("12345", addedEndpoint.EndpointSerialNumber);
        }

        [Fact]
        public void AddEndpoint__WhenParametersAreWrong__ItShouldThrowException()
        {
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);
            var invalidMeterModelId = "INVALID";

            var exception = Assert.Throws<Exception>(() => service.ConvertMeterModelId(invalidMeterModelId));
            Assert.Equal("Invalid Meter Model ID.", exception.Message);
        }

        [Fact]
        public void EditEndpoint_WhenParametersAreCorrect_ItShouldUpdateSwitchStateSuccessfully()
        {
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

            service.EditEndpoint("12345", 1);

            var updatedEndpoint = repository.GetBySerialNumber("12345");
            Assert.NotNull(updatedEndpoint);
            Assert.Equal(1, updatedEndpoint.SwitchState);
        }

        [Fact]
        public void FindEndpoint_WhenEndpointSerialNumberIsCorrect_ItShouldReturnCorrectEndpoint()
        {
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

            var foundEndpoint = service.FindEndpoint("12345");

            Assert.NotNull(foundEndpoint);
            Assert.Equal("12345", foundEndpoint.EndpointSerialNumber);
        }
    }
}
