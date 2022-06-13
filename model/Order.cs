using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Service.Context;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils;

namespace YTDSSTGenII.Forms.model
{
   public class Order
    {
        public Order() {

        }

        public Order(UserMotor[] umArray)
        {
            this.OutTradeNo = Guid.NewGuid().ToString();
            this.TerminalCode = GlobalParmeters.machineNumber;
            this.PayType = 1;

            this.TotalNum = UserContext.getInstance().getBuyLotteryNum();
            this.TotalMoney = UserContext.getInstance().getBuyMoney();

            this.OutSuccNum = 0;
            this.OutSuccMoney = 0;

            this.OutErrorNum = 0;
            this.OutErrorMoney = 0;

            this.OrderState = 1;
            this.CreateTime = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            this.TicketOutState = 1;
            this.deleted = 0;

            Tickets = new List<OrderDetails>();
            foreach (UserMotor um in umArray)
            {
                for (int i = 0; i < um.BuyLotteryNum; i++)
                {
                    OrderDetails t = new OrderDetails();
                    t.TerminalOrderId = 0;
                    t.OutTradeNo = this.OutTradeNo;
                    t.SelfTerminalId = 1;
                    t.TerminalCode = GlobalParmeters.machineNumber;
                    t.HeadNo =  Convert.ToInt32(um.MotorId);
                    t.TicketId = um.MotorId+"-"+(i+1).ToString();
                    t.TicketOutTime = "";

                    t.LotteryTypeId = um.Lottery.LotteryId;
                    t.LotteryName = um.Lottery.LotteryName;

                    t.TicketOutState = 0;
                    t.UnitPrice = um.Lottery.UnitPrice;
                    t.Deleted = 0;

                    Tickets.Add(t);
                }
            }
        }

        public Order(C006ContentAck ack)
        {
            this.OutTradeNo = ack.OrderId;
            this.TerminalCode = GlobalParmeters.machineNumber;
            this.PayType = 2;

            this.TotalNum = UserContext.getInstance().getBuyLotteryNum();
            this.TotalMoney = UserContext.getInstance().getBuyMoney();

            this.OutSuccNum = 0;
            this.OutSuccMoney = 0;

            this.OutErrorNum = 0;
            this.OutErrorMoney = 0;

            this.OrderState = 0;
            this.CreateTime = System.DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            this.TicketOutState = 0;
            this.deleted = 0;

            Tickets = new List<OrderDetails>();

            foreach (AckTicket item in ack.tickets)
            {
                foreach (String ids in item.TicketIds)
                {
                    OrderDetails t = new OrderDetails();
                    t.TerminalOrderId = 0;
                    t.OutTradeNo = ack.OrderId;
                    t.SelfTerminalId = 1;
                    t.TerminalCode = GlobalParmeters.machineNumber;
                    t.HeadNo = Convert.ToInt32(item.HeadNo);
                    t.TicketId = ids;
                    t.TicketOutTime = "";

                    t.LotteryTypeId = item.LotteryTypeId;
                    t.LotteryName = UserContext.getInstance().UserMotorArray[Convert.ToInt32(item.HeadNo)-1].Lottery.LotteryName;
                    
                    t.TicketOutState = 0;                    
                    t.UnitPrice = UserContext.getInstance().UserMotorArray[Convert.ToInt32(item.HeadNo) - 1].Lottery.UnitPrice;
                    t.Deleted = 0;

                    Tickets.Add(t);
                }
            }
        }

        public String OutTradeNo { get; set; }//订单编号
        public String TerminalCode { get; set; } //终端编号
        public Int32 PayType { get; set; }//支付方式
        public List<OrderDetails> Tickets { get; set; }

        public int TotalNum { get; set; }//总票数量
        public int TotalMoney { get; set; }//总金额

        public String CreateTime { get; set; }//创建时间
        public String EndTime { get; set; }//结束时间
        public String PayTime { get; set; }//支付时间
        public String UploadTime { get; set; }//上传时间
        public int OrderState { get; set; }
        public int OutSuccNum { get; set; }
        public int OutSuccMoney { get; set; }

        public int OutErrorNum { get; set; }
        public int OutErrorMoney { get; set; }

        public int TicketOutState { get; set; }//状态——0-初始状态、1-全部成功、2-部分成功、3-全部失败

        public int getFeedbackTicketOutState() {
                if (TicketOutState == 3)
                {
                return 0; //失败
            }
            else if (TicketOutState == 2)
            {
                return 1;//成功
            }
            return 2; //部分成功
        }

        public int deleted { get; set; }//是否删除
    }
}
