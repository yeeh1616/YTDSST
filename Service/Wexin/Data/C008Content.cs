using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C008ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C008";

        public String OrderId { get; set; }//订单号
        public int OrderCutpaperStatus { get; set; }//出票状态
        public List<ticket008> tickets { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C008ContentReq[");
            foreach (ticket008 item in tickets)
            {
                sb.Append("HeadNo=").Append(item.HeadNo).Append(",");
                sb.Append("LotteryTypeId=").Append(item.LotteryTypeId).Append(",");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }

    public class ticket008
    {
        //机头
        public String HeadNo { get; set; }

        //彩票的ID
        public String LotteryTypeId { get; set; }

        public String TicketId { get; set; }

       public int TicketCutpaperStatus { get; set; }
    }

    public class C008ContentAck : Content
    {
        //订单ID
        public String OrderId { get; set; }

        public String ErrorMsg { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C008ContentAck[");
            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
