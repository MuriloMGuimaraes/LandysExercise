using System;
using Application.UseCases;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new EndpointRepository();
            var service = new EndpointService(repository);

            while (true)
            {
                Console.WriteLine("\n=== Endpoint Management ===");
                Console.WriteLine("1) Insert a new endpoint");
                Console.WriteLine("2) Edit an existing endpoint");
                Console.WriteLine("3) Delete an existing endpoint");
                Console.WriteLine("4) List all endpoints");
                Console.WriteLine("5) Find an endpoint by Serial Number");
                Console.WriteLine("0) Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();
                try
                {
                    switch (option)
                    {
                        case "1":
                            InsertEndpoint(service);
                            break;
                        case "2":
                            EditEndpoint(service);
                            break;
                        case "3":
                            DeleteEndpoint(service);
                            break;
                        case "4":
                            ListEndpoints(service);
                            break;
                        case "5":
                            FindEndpoint(service);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void InsertEndpoint(EndpointService service)
        {
            Console.Write("Enter Endpoint Serial Number (1-15 characters): ");
            string serialNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(serialNumber) || serialNumber.Length > 15)
                throw new Exception("Serial Number must be between 1 and 15 characters.");

            Console.Write("Enter Meter Model ID (NSX1P2W, NSX1P3W, NSX2P3W, NSX3P4W): ");
            string meterModelInput = Console.ReadLine();
            int meterModelId = service.ConvertMeterModelId(meterModelInput);

            Console.Write("Enter Meter Number (positive, non-zero): ");
            if (!int.TryParse(Console.ReadLine(), out int meterNumber) || meterNumber <= 0)
                throw new Exception("Meter Number must be a positive, non-zero integer.");

            Console.Write("Enter Meter Firmware Version (more than 1 character): ");
            string firmwareVersion = Console.ReadLine();
            if (string.IsNullOrEmpty(firmwareVersion) || firmwareVersion.Length <= 1)
                throw new Exception("Firmware Version must have more than 1 character.");

            Console.Write("Enter Switch State (Disconnected, Connected, Armed): ");
            string switchStateInput = Console.ReadLine();
            int switchState = switchStateInput.ToLower() switch
            {
                "disconnected" => 0,
                "connected" => 1,
                "armed" => 2,
                _ => throw new Exception("Invalid Switch State. Allowed values are: Disconnected, Connected, Armed.")
            };

            var endpoint = new Endpoint
            {
                EndpointSerialNumber = serialNumber,
                MeterModelId = meterModelId,
                MeterNumber = meterNumber,
                MeterFirmwareVersion = firmwareVersion,
                SwitchState = switchState
            };

            service.AddEndpoint(endpoint);
            Console.WriteLine("Endpoint added successfully.");
        }

        static void EditEndpoint(EndpointService service)
        {
            Console.Write("Enter Endpoint Serial Number to edit: ");
            string serialNumber = Console.ReadLine();

            Console.Write("Enter new Switch State (Disconnected, Connected, Armed): ");
            string switchStateInput = Console.ReadLine();
            int newSwitchState = switchStateInput.ToLower() switch
            {
                "disconnected" => 0,
                "connected" => 1,
                "armed" => 2,
                _ => throw new Exception("Invalid Switch State. Allowed values are: Disconnected, Connected, Armed.")
            };

            service.EditEndpoint(serialNumber, newSwitchState);
            Console.WriteLine("Endpoint updated successfully.");
        }

        static void DeleteEndpoint(EndpointService service)
        {
            Console.Write("Enter Endpoint Serial Number to delete: ");
            string serialNumber = Console.ReadLine();
            service.DeleteEndpoint(serialNumber);
            Console.WriteLine("Endpoint deleted successfully.");
        }

        static void ListEndpoints(EndpointService service)
        {
            var endpoints = service.ListEndpoints();
            Console.WriteLine("\n=== All Endpoints ===");
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine($"Serial: {endpoint.EndpointSerialNumber}, Model ID: {endpoint.MeterModelId}, Meter Number: {endpoint.MeterNumber}, Firmware: {endpoint.MeterFirmwareVersion}, Switch State: {endpoint.SwitchState}");
            }
        }

        static void FindEndpoint(EndpointService service)
        {
            Console.Write("Enter Endpoint Serial Number to find: ");
            string serialNumber = Console.ReadLine();
            var endpoint = service.FindEndpoint(serialNumber);
            Console.WriteLine($"\nSerial: {endpoint.EndpointSerialNumber}, Model ID: {endpoint.MeterModelId}, Meter Number: {endpoint.MeterNumber}, Firmware: {endpoint.MeterFirmwareVersion}, Switch State: {endpoint.SwitchState}");
        }
    }
}
