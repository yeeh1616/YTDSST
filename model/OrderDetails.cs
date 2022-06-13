using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.model
{
   public class OrderDetails
    {
        public Int32 TerminalOrderId { get; set; } //订单Id
        public String OutTradeNo { get; set; } //
        public Int32 SelfTerminalId { get; set; }
        public String TerminalCode { get; set; }
        public int HeadNo { private get; set; } //马达Id
        public String TicketId { get; set; } //票Id
        public String TicketOutTime { get; set; }
        public String LotteryTypeId { get; set; } //彩种Id
        public String LotteryName { get; set; }//彩种名称
        public int UnitPrice { get; set; }//单价
        public int Ticket_Num { get; set; }//售票数量
        public int TicketOutState { get; set; }  //状态
        public int getFeedBackStatus()
        {
            return TicketOutState == 3 ? 0 : 1;
        }
        public int Deleted { get; set; }  //删除状态

        public int getHeadNo_Int() {
            return this.HeadNo;
        }

        public String getHeadNo_String() {
            return this.HeadNo.ToString("00");
        }
    }
}
