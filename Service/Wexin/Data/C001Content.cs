using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C001ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C001";

        //机头
        public String HeadNo { get; set; }

        //彩票的ID
        public String LotteryTypeId { get; set; }

        //终端机编号
        public String TerminalCode { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C001ContentReq[");
            sb.Append("HeadNo=").Append(this.HeadNo).Append(",");
            sb.Append("LotteryTypeId=").Append(this.LotteryTypeId).Append(",");
            sb.Append("TerminalCode=").Append(this.TerminalCode);

            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C001ContentAck : Content
    {
        //二维码字串
        public String QRCodeUrl { get; set; }
        //订单ID
        public String OrderId { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C001ContentAck[");

            sb.Append("QRCodeUrl=").Append(this.QRCodeUrl).Append(",");
            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
