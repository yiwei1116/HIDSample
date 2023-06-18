using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIDCommunication.Define
{
    public interface ICommandLayer
    {
        Task<byte[]> I2CWrite(int slaveAddress, int address, byte data);
        Task<byte[]> I2CRead(int slaveAddress, int address);
    }
}
