using System;
using System.Collections.Generic;
using System.Linq;
using EndpointManagement.Application.UseCases;
using EndpointManagement.Domain.Entities;
using EndpointManagement.Infrastructure.Repositories;
using Xunit;

namespace Tests
{
    public class ManagerEndpointServiceTests
    {
        [Fact]
        public void AddListDeleteEndpoint_WhenParametersAreCorrect_ShouldWorkCorrectly()
        {
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

            service.AddEndpoint(endpoint);

            var allEndpointsAfterAdd = service.ListEndpoints();
            Assert.NotNull(allEndpointsAfterAdd);
            Assert.Single(allEndpointsAfterAdd);
            Assert.Equal("67890", allEndpointsAfterAdd.First().EndpointSerialNumber);

            service.DeleteEndpoint("67890");

            var allEndpointsAfterDelete = service.ListEndpoints();
            Assert.NotNull(allEndpointsAfterDelete);
            Assert.Empty(allEndpointsAfterDelete);
        }
    }
}
