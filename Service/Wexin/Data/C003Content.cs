using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C003ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C003";

        /**
         * 订单ID
         */ 
        public String OrderId {get; set;}
        /**
         * 订单状态
         * 0-未支付
         * 1-支付成功
         */ 
        public int OrderState { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C003ContentReq[");
            sb.Append("OrderId=").Append(this.OrderId).Append(",");
            sb.Append("OrderState=").Append(this.OrderState);

            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C003ContentAck : Content
    {
        /**
         * 订单ID
         */ 
        public String OrderId { get; set; }

        /**
         * 出票状态
         */ 
        public int CutpaperState { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C003ContentAck[");
            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
