using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIDCommunication.Define
{
    /// <summary>
    /// Packet Processing Layer
    /// This layer is responsible for composing and decomposing packets.
    /// </summary>
    public interface IPacketComposer
    {
        byte[] Compose(byte[] data);
        byte[] Decompose(byte[] packet);
    }
}
