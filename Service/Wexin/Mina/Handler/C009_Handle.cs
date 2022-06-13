using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
    class C009_Handle : IProtocolHandle
    {
        public C009_Handle()
        {
        }

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C009ContentAck ack = JsonConvert.DeserializeObject<C009ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = ack;

            return remoteData;
        }
    }
}
