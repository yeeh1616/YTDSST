using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Mina.Filter.Codec;
using Mina.Core.Buffer;

using log4net;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service.Wexin.Mina
{
    public class WexinEncoder : ProtocolEncoderAdapter
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WexinEncoder));


        public override void Encode(global::Mina.Core.Session.IoSession session, object message, IProtocolEncoderOutput output)
        {
            logger.Debug("Begin To Encode Packet :" + message);

            WexinPacket packet = (WexinPacket)message;
            byte[] byteContent = Encoding.Default.GetBytes(packet.jsonContent);

            MemoryStream ms = new MemoryStream();
            //包头 
            ms.WriteByte(WexinPacket.PACKET_START_FLAG);
            //包的长度
            int packetLength = WexinPacket.PACKET_LEN + 
                WexinPacket.PACKET_START_FLAG_LEN + 
                WexinPacket.PACKET_PROTOCOL_ID_LEN +
                WexinPacket.PACKET_SIGNATURE_LEN +
                WexinPacket.PACKET_MACHINE_CODE_LEN + 
                byteContent.Length;

            ms.Write(ByteUtils.ShortToByte((short)packetLength), 0, WexinPacket.PACKET_LEN);
            
            //协议类型
            ms.Write(Encoding.Default.GetBytes(packet.ProtocolId), 0, WexinPacket.PACKET_PROTOCOL_ID_LEN);
            //机器编码
            ms.Write(Encoding.Default.GetBytes(packet.MachineCode), 0, WexinPacket.PACKET_MACHINE_CODE_LEN);
            //内容
            ms.Write(byteContent, 0, byteContent.Length);
            //签名
            ms.Write(Encoding.Default.GetBytes(packet.Signature), 0, WexinPacket.PACKET_SIGNATURE_LEN);

            byte[] bytePacket = ms.ToArray();

            IoBuffer buffer = IoBuffer.Allocate(bytePacket.Length);
            buffer.AutoExpand = true;
            buffer.Put(bytePacket);
           
            buffer.Flip();
            
            output.Write(buffer);
            output.Flush();
            logger.Debug("End To Encode Packet :" + message);

            //ms.Close();
        }
    }
}
