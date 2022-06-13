using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C006ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C006";

        public int pay_type;//支付类型：现金，微信，支付宝
        public List<ticket> tickets { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C006ContentReq[");
            foreach (ticket item in tickets)
            {
                sb.Append("HeadNo=").Append(item.HeadNo).Append(",");
                sb.Append("LotteryTypeId=").Append(item.LotteryTypeId).Append(",");
                sb.Append("Nums=").Append(item.Nums);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }

    public class ticket
    {
        //机头
        public String HeadNo { get; set; }

        //彩票的ID
        public String LotteryTypeId { get; set; }

        //终端机编号
        public int Nums { get; set; }
    }

    public class AckTicket : ticket
    {
        public List<String> TicketIds { get; set; }
        //public String TicketIds { get; set; }
    }

    public class C006ContentAck : Content
    {
        //二维码字串
        public String QRCodeUrl { get; set; }
        //订单ID
        public String OrderId { get; set; }

        public String ErrorMsg { get; set; }

        public List<AckTicket> tickets { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C006ContentAck[");

            sb.Append("QRCodeUrl=").Append(this.QRCodeUrl).Append(",");
            sb.Append("OrderId=").Append(this.OrderId);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
