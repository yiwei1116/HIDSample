using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HIDCommunication
{
    /// <summary>
    /// Data Layer
    /// This layer is responsible for handling the low-level communication with the HID device.
    /// </summary>
    public class HIDCommunicator
    {
        private HidDevice _device;
        private TaskCompletionSource<byte[]> _tcs;

        public void Connect(int vendorId, int productId)
        {
            // Logic to connect to the HID device
            _device = HidDevices.Enumerate(vendorId, productId).FirstOrDefault();
            if (_device != null)
            {
                _device.OpenDevice();
                _device.Inserted += DeviceAttachedHandler;
                _device.Removed += DeviceRemovedHandler;
                _device.MonitorDeviceEvents = true;

                _device.ReadReport(OnReportReceived); // Start listening for incoming reports
            }
        }

        public void Disconnect()
        {
            if (_device != null)
            {
                _device.CloseDevice();
            }
        }

        private void OnReportReceived(HidReport report)
        {
            // Trigger the completion source with the data received
            _tcs?.TrySetResult(report.Data);

            // Continue listening for the next report
            _device?.ReadReport(OnReportReceived);
        }
        public Task<byte[]> WriteDataAndWaitForResponseAsync(byte[] data)
        {
            _tcs = new TaskCompletionSource<byte[]>();

            // Write the data to the device
            if (_device != null)
            {
                var hidReport = new HidReport(data.Length, new HidDeviceData(data, HidDeviceData.ReadStatus.Success));
                _device.WriteReport(hidReport);
            }

            // Return the task that will complete once the response is received
            return _tcs.Task;
        }
        private void DeviceAttachedHandler()
        {
            // Reinitialize connection
            _device.OpenDevice();

            // Update UI
            //UpdateConnectionStatus(true);

            // Log the event
            //Log("Device attached");

            // Start receiving data
            _device.ReadReport(OnReportReceived);
        }

        private void DeviceRemovedHandler()
        {
            // Cleanup resources
            _device.CloseDevice();

            // Update UI
            //UpdateConnectionStatus(false);

            // Log the event
            //Log("Device removed");

            // Handle application-specific logic
            //PauseOperations();
        }
    }
}
