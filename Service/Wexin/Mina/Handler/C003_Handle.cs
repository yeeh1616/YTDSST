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
    public class C003_Handle : IProtocolHandle
    {
        public Data.RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C003ContentReq req = JsonConvert.DeserializeObject<C003ContentReq>(packet.jsonContent, jsetting);

            remoteData.Content = req;

            return remoteData;
        }
    }
}
