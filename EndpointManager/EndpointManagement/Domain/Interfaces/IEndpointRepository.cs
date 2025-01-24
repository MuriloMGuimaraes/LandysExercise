using System.Collections.Generic;
using EndpointManagement.Domain.Entities;

namespace EndpointManagement.Domain.Interfaces
{
    public interface IEndpointRepository
    {
        void Add(Endpoint endpoint);
        void Update(Endpoint endpoint);
        void Delete(string serialNumber);
        Endpoint Find(string serialNumber);
        IEnumerable<Endpoint> GetAll();
    }
}
