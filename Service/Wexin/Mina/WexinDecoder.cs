using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mina.Filter.Codec;

using log4net;

using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Service.Wexin.Mina
{
    public class WexinDecoder : CumulativeProtocolDecoder
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WexinDecoder));


        protected override bool DoDecode(global::Mina.Core.Session.IoSession session, global::Mina.Core.Buffer.IoBuffer input, IProtocolDecoderOutput output)
        {
            logger.Debug("in.remaining : " + input.Remaining);
            if (input.Remaining > 0)
            {
                input.Mark();

                byte flag = input.Get();
                if (flag == WexinPacket.PACKET_START_FLAG)
                {
                    //解析包的长度
                    byte[] byteSize = new byte[WexinPacket.PACKET_LEN];
                    input.Get(byteSize, 0, WexinPacket.PACKET_LEN);
                    short packetLen = ByteUtils.ByteToShort(byteSize);
                    logger.Debug("Packet Length :" + packetLen);

                    input.Reset();

                    if (packetLen > input.Remaining)
                    {
                        //当前流不足一个数据包
                        return false;
                    }
                    else
                    {
                        input.Get();
                        input.Get(byteSize, 0, WexinPacket.PACKET_LEN);
                        //获取协议的ID
                        byte[] byteProtocolId = new byte[WexinPacket.PACKET_PROTOCOL_ID_LEN];
                        input.Get(byteProtocolId, 0, WexinPacket.PACKET_PROTOCOL_ID_LEN);
                        String protocolId = Encoding.Default.GetString(byteProtocolId);

                        //机器编码
                        byte[] byteMachineCode = new byte[WexinPacket.PACKET_MACHINE_CODE_LEN];
                        input.Get(byteMachineCode, 0, byteMachineCode.Length);
                        String machineCode = Encoding.Default.GetString(byteMachineCode);

                        //协议的内容
                        byte[] byteContent = new byte[packetLen - WexinPacket.PACKET_START_FLAG_LEN - WexinPacket.PACKET_PROTOCOL_ID_LEN - WexinPacket.PACKET_LEN - WexinPacket.PACKET_SIGNATURE_LEN - WexinPacket.PACKET_MACHINE_CODE_LEN];
                        input.Get(byteContent, 0, byteContent.Length);
                        String content = Encoding.UTF8.GetString(byteContent);

                        //签名
                        byte[] byteSignature = new byte[WexinPacket.PACKET_SIGNATURE_LEN];
                        input.Get(byteSignature, 0, byteSignature.Length);
                        String signature = Encoding.Default.GetString(byteSignature);

                        WexinPacket packData = new WexinPacket();
                        packData.Signature = signature;
                        packData.jsonContent = content;
                        packData.MachineCode = machineCode;
                        packData.ProtocolId = protocolId;

                        output.Write(packData);

                        if (input.Remaining > 0)
                        {
                            //继续处理
                            return true;
                        }
                    }
                }
                else
                {
                    //unkown protocol, close
                    session.Close(true);
                }
            }

            //数据长度不够
            return false;
        }
    }
}
