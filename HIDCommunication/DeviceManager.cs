using HIDCommunication.Define;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace HIDCommunication
{
    /// <summary>
    /// Business Layer
    /// This layer contains the business logic and interacts with the Packet Processing Layer and the Data Layer.
    /// </summary>
    public class DeviceManager
    {
        private readonly HIDCommunicator _communicator;

        public DeviceManager()
        {
            _communicator = new HIDCommunicator();
        }

        public void Connect(int vendorId, int productId)
        {
            _communicator.Connect(vendorId, productId);
        }

        public void Disconnect()
        {
            _communicator.Disconnect();
        }

        public async Task<byte[]> SendDataAndWaitForResponseAsync(byte[] data, IPacketComposer packetComposer)
        {
            byte[] composedPacket = packetComposer.Compose(data);
            byte[] receivedPacket = await _communicator.WriteDataAndWaitForResponseAsync(composedPacket);
            return packetComposer.Decompose(receivedPacket);
        }
    }
}
