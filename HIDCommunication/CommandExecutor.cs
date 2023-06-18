using HIDCommunication.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIDCommunication
{
    public class CommandExecutor : ICommandLayer
    {
        private readonly DeviceManager _deviceManager;
        private readonly IPacketComposer _writePacketComposer = new I2CWritePacketComposer();
        private readonly IPacketComposer _readPacketComposer = new I2CReadPacketComposer();

        public CommandExecutor(DeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        public async Task<byte[]> I2CWrite(int slaveAddress, int address, byte data)
        {
            byte[] rawData = {/* Combine slaveAddress, address, and data into raw byte array */};
            return await _deviceManager.SendDataAndWaitForResponseAsync(rawData, _writePacketComposer);
        }

        public async Task<byte[]> I2CRead(int slaveAddress, int address)
        {
            byte[] rawData = {/* Combine slaveAddress and address into raw byte array */};
            return await _deviceManager.SendDataAndWaitForResponseAsync(rawData, _readPacketComposer);
        }
    }
}
