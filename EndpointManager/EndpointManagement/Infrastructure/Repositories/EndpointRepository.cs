using System;
using System.Collections.Generic;
using System.Linq;
using EndpointManagement.Domain.Entities;
using EndpointManagement.Domain.Interfaces;

namespace EndpointManagement.Infrastructure.Repositories
{
    public class EndpointRepository : IEndpointRepository
    {
        private readonly List<Endpoint> _endpoints = new();

        public void Add(Endpoint endpoint)
        {
            if (_endpoints.Any(e => e.EndpointSerialNumber == endpoint.EndpointSerialNumber))
                throw new Exception("Endpoint with this serial number already exists.");

            _endpoints.Add(endpoint);
        }

        public void Update(Endpoint endpoint)
        {
            var existingEndpoint = _endpoints.FirstOrDefault(e => e.EndpointSerialNumber == endpoint.EndpointSerialNumber);
            if (existingEndpoint == null)
                throw new Exception("Endpoint not found.");

            existingEndpoint.SwitchState = endpoint.SwitchState;
        }

        public void Delete(string serialNumber)
        {
            var endpoint = _endpoints.FirstOrDefault(e => e.EndpointSerialNumber == serialNumber);
            if (endpoint == null)
                throw new Exception("Endpoint not found.");

            _endpoints.Remove(endpoint);
        }

        public Endpoint Find(string serialNumber)
        {
            return _endpoints.FirstOrDefault(e => e.EndpointSerialNumber == serialNumber)
                   ?? throw new Exception("Endpoint not found.");
        }

        public IEnumerable<Endpoint> GetAll()
        {
            return _endpoints;
        }

    public Endpoint GetBySerialNumber(string serialNumber)
    {
        return _endpoints.FirstOrDefault(e => e.EndpointSerialNumber == serialNumber);
    }
    }
}
