using System;
using System.Collections.Generic;
using EndpointManagement.Domain.Entities;
using EndpointManagement.Domain.Interfaces;

namespace EndpointManagement.Application.UseCases
{
    public class EndpointService
    {
        private readonly IEndpointRepository _repository;

        public EndpointService(IEndpointRepository repository)
        {
            _repository = repository;
        }

        public void AddEndpoint(Endpoint endpoint)
        {
            _repository.Add(endpoint);
        }

        public void EditEndpoint(string serialNumber, int newSwitchState)
        {
            var endpoint = _repository.Find(serialNumber);
            endpoint.SwitchState = newSwitchState;
            _repository.Update(endpoint);
        }

        public void DeleteEndpoint(string serialNumber)
        {
            _repository.Delete(serialNumber);
        }

        public IEnumerable<Endpoint> ListEndpoints()
        {
            return _repository.GetAll().OrderBy(e => e.EndpointSerialNumber);
        }

        public Endpoint FindEndpoint(string serialNumber)
        {
            return _repository.Find(serialNumber);
        }

        public int ConvertMeterModelId(string meterModel)
        {
            return meterModel switch
            {
                "NSX1P2W" => 16,
                "NSX1P3W" => 17,
                "NSX2P3W" => 18,
                "NSX3P4W" => 19,
                _ => throw new Exception("Invalid Meter Model ID.")
            };
        }
    }
}
