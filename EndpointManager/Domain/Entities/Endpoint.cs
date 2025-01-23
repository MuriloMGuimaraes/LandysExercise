// Domain/Entities/Endpoint.cs
namespace Domain.Entities
{
    public class Endpoint : Device
    {
        public int MeterModelId { get; set; }
        public int SwitchState { get; set; }
    }
}
