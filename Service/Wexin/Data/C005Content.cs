using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Service.Wexin.Data;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class C005ContentReq : Content
    {
        public static readonly String PROTOCOL_TYPE = "C005";

        public String OrderId { get; set; }
        public int OrderCutpaperStatus { get; set; }

        public List<Ticket> Tickets { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C005ContentReq[");
            sb.Append("OrderId=").Append(this.OrderId).Append(",");
            sb.Append("OrderCutpaperStatus=").Append(this.OrderCutpaperStatus).Append(",");

            sb.Append("Tickets[");

            if (this.Tickets != null && this.Tickets.Count > 0)
            {
                foreach (Ticket t in this.Tickets)
                {
                    sb.Append(t.ToString()).Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("]");

            sb.Append("]");

            return sb.ToString();
        }
    }

    public class C005ContentAck : Content
    {
        public String OrderId { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("C005ContentAck[");
            sb.Append("OrderId=").Append(this.OrderId);
            sb.Append("]");

            return sb.ToString();
        }
    }

    public class Ticket {
        public String TicketId {get; set;}
        public int TicketCutpaperStatus { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Ticket[");

            sb.Append("TicketId=").Append(this.TicketId).Append(",");
            sb.Append("TicketCutpaperStatus=").Append(this.TicketCutpaperStatus);

            sb.Append("]");

            return sb.ToString();
        }
    }
}
