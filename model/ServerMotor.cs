using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.model
{
    public class ServerMotor:BaseMotor
    {
        private int trace_lottery_num;//剩余彩票数量

        public ServerMotor (String motorId,lottery l,int lnum ) {
            this.MotorId = motorId;
            this.Lottery = l;
            this.TraceLotteryNum = lnum;
        }

        public int TraceLotteryNum
        {
            get { return trace_lottery_num; }
            set { trace_lottery_num = value; }
        }
    }
}
