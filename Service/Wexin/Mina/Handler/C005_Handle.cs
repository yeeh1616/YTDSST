using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft;
using Newtonsoft.Json;

using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
    public class C005_Handle : IProtocolHandle
    {

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C005ContentAck req = JsonConvert.DeserializeObject<C005ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = req;

            return remoteData;
        }
    }
}
