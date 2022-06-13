using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service.Wexin.Data
{
    public class RemoteData
    {
        public String MachineCode{get; set;}
        public String ProtocolId { get; set; }
        public Content Content { get; set; }

        public override String ToString() {
            StringBuilder sb = new StringBuilder();

            sb.Append("RemoteData[");

            sb.Append("MachineCode=").Append(this.MachineCode).Append(",");
            sb.Append("ProtocolId=").Append(this.ProtocolId).Append(",");
            sb.Append("Content=").Append(this.Content.ToString());
            sb.Append("]");

            return sb.ToString();
        }
    }
}
