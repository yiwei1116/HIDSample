using HIDCommunication.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIDCommunication
{
    public class I2CReadPacketComposer : IPacketComposer
    {
        public byte[] Compose(byte[] data)
        {
            // Logic for composing the I2C Read packet
            throw new NotImplementedException();
        }

        public byte[] Decompose(byte[] packet)
        {
            // Logic for decomposing the I2C Read packet
            throw new NotImplementedException();
        }
    }
}
