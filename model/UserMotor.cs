using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.model
{
   public class UserMotor:BaseMotor
    {
        private int buy_lottery_num;//购买的彩票数量

        public UserMotor (String motorId, lottery l, int bnum ) {
            this.MotorId = motorId;
            this.Lottery = l;
            this.BuyLotteryNum = bnum;
        }

        public int BuyLotteryNum
        {
            get { return buy_lottery_num; }
            set { buy_lottery_num = value; }
        }
    }
}
