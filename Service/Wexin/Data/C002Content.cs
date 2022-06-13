using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C002ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C002";

        /**
         * 订单ID
         */ 
        public String OrderId { get; set; }
        /**
         * 扫码状态
         * 0:未扫码
         * 1:已扫码
         */ 
        public int ScanState { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C002ContentReq[");

            sb.Append("OrderId=").Append(this.OrderId).Append(",");
            sb.Append("ScanState=").Append(this.ScanState);

            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C002ContentAck : Content
    {
        //订单ID
        public String OrderId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C002ContentReq[");

            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
