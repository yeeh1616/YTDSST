using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C007ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C007";

        //订单号
        public String OrderId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C007ContentReq[");
            sb.Append("OrderId=").Append(this.OrderId);
            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C007ContentAck : Content
    {
        //二维码字串
        public String OrderState { get; set; }
        //订单ID
        public String OrderId { get; set; }
        public String ErrorMsg { get; set; }
        public int PayType { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C007ContentAck[");

            sb.Append("OrderState=").Append(this.OrderState).Append(",");
            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
