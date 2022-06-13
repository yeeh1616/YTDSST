using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.model
{
   public class lottery
    {
        private String lottery_id;//彩种id        
        private String lottery_name;//彩票名称
        private int ticket_length;//票长度        
        private int unit_price;//单价

        public lottery (String lname,int tlength,int uprice )
        {
            this.lottery_name = lname;
            this.ticket_length = tlength;
            this.unit_price = uprice;
        }

        public String LotteryId
        {
            get { return lottery_id; }
            set { lottery_id = value; }
        }
        public String LotteryName
        {
            get { return lottery_name; }
        }

        public int TicketLength
        {
            get { return ticket_length; }
            set { ticket_length = value; }
        }
        
        public int UnitPrice
        {
            get { return unit_price; }
        }
    }
}
