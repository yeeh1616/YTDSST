using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
   public class C008_Handle : IProtocolHandle
    {
        public C008_Handle()
        {
        }

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C008ContentAck ack = JsonConvert.DeserializeObject<C008ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = ack;

            return remoteData;
        }
    }
}
