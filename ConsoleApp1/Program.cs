
using HIDCommunication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    
    public class Model
    {
        public string ID { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
       
        public int HexAge { get; set; }
    }
    class Program
    {

        public class ClassA
        {
            public int Value { get; set; }
        }

        public class ClassB
        {
            public string Message { get; set; }
        }

        static async Task Main()
        {
            DeviceManager deviceManager = new DeviceManager();
            deviceManager.Connect(0x1234, 0x4567);

            CommandExecutor commandExecutor = new CommandExecutor(deviceManager);

            // I2C Write
            int slaveAddress =0x67 ;
            int address = 0x11;
            byte data = 0x22;
            var response = await commandExecutor.I2CWrite(slaveAddress, address, data);

            // I2C Read
            response = await commandExecutor.I2CRead(slaveAddress, address);

            deviceManager.Disconnect();

            //var cancellationTokenSource = new CancellationTokenSource();
            //var cancellationToken = cancellationTokenSource.Token;

            //// Cancel the task after 2 seconds
            //cancellationTokenSource.CancelAfter(5000);

            //try
            //{
            //    var result = await ProcessDataAsync(cancellationToken);
            //    Console.WriteLine(result.Message);
            //}
            //catch (OperationCanceledException)
            //{
            //    Console.WriteLine("Operation was canceled.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //}
            Console.ReadLine();
        }
        static async Task<ClassB> ProcessDataAsync(CancellationToken cancellationToken)
        {
            // Simulate fetching ClassA asynchronously
            ClassA classA = await FetchClassAAsync(cancellationToken);

            // Process data from ClassA and return as ClassB
            return new ClassB { Message = $"Processed value: {classA.Value}" };
        }

        static async Task<ClassA> FetchClassAAsync(CancellationToken cancellationToken)
        {
            // Simulate a delay
            await Task.Delay(3000, cancellationToken);

            // Return a ClassA instance
            return new ClassA { Value = 10 };
        }
    }
  
  
}
