using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
   public class C007_Handle : IProtocolHandle
    {
        public C007_Handle()
        {
        }

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C007ContentAck ack = JsonConvert.DeserializeObject<C007ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = ack;

            return remoteData;
        }
    }
}
