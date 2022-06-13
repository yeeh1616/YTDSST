using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C004ContentReq : Content 
    {
        public static readonly String PROTOCOL_TYPE = "C004";

        public String TerminalCode { get; set; }
        public String OrderId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C004ContentReq[");

            sb.Append("OrderId=").Append(this.OrderId).Append(",");
            sb.Append("TerminalCode=").Append(this.TerminalCode);

            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C004ContentAck : Content
    {
        public String Echo { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C004ContentAck[");
            sb.Append("Echo=").Append(this.Echo);
            sb.Append("]");

            return sb.ToString();
        }
    }
}
