using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin
{
    /**
     * 微信支付协议包描述
     */ 
    public class WexinPacket 
    {
        //包开始标志
        public static readonly byte PACKET_START_FLAG = 0x7E;

        //包标志位长度
        public static readonly int PACKET_START_FLAG_LEN = 1;

        //包的长度位的长度
        public static readonly int PACKET_LEN = 2;

        //协议类型的占位长度
        public static readonly int PACKET_PROTOCOL_ID_LEN = 4;

        //机器码的占位长度
        public static readonly int PACKET_MACHINE_CODE_LEN =13;

        //签名的占位符长度
        public static readonly int PACKET_SIGNATURE_LEN = 32;

        //协议类型
        public String ProtocolId { set; get; }

        //包签名
        public String Signature { set; get;}

        //协议内容
        public String jsonContent { set; get; }

        //机器编码
        public String MachineCode { set; get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("WexinPacket").Append("[");

            sb.Append("ProtocolId=").Append(ProtocolId).Append(",");
            sb.Append("MachineCode=").Append(MachineCode).Append(",");
            sb.Append("Content=").Append(jsonContent).Append(",");
            sb.Append("Signature=").Append(Signature).Append("");

            sb.Append("]");

            return sb.ToString();
        }
    }
}
