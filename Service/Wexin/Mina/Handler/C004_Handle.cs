using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft;
using Newtonsoft.Json;

using YTDSSTGenII.Utils.Newtonsoft;
using YTDSSTGenII.Service.Wexin.Data;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
    public class C004_Handle : IProtocolHandle
    {
        public Data.RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C004ContentAck req = JsonConvert.DeserializeObject<C004ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = req;
            return remoteData;
        }
    }
}
