// Domain/Entities/Device.cs
namespace Domain.Entities
{
    public abstract class Device
    {
        public string EndpointSerialNumber { get; set; }
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
    }
}
