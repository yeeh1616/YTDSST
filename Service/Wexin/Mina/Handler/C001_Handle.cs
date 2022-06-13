using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using YTDSSTGenII.Service.Wexin.Data;
using YTDSSTGenII.Utils.Newtonsoft;

namespace YTDSSTGenII.Service.Wexin.Mina.Handler
{
    /**
     * 请求服务器生成二维码
     */ 
    public class C001_Handle : IProtocolHandle
    {
        public C001_Handle()
        {
        }

        public RemoteData DoHandle(WexinPacket packet)
        {
            RemoteData remoteData = new RemoteData();

            remoteData.MachineCode = packet.MachineCode;
            remoteData.ProtocolId = packet.ProtocolId;

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new UnderlineSplitContractResolver();

            C001ContentAck ack = JsonConvert.DeserializeObject<C001ContentAck>(packet.jsonContent, jsetting);

            remoteData.Content = ack;
                
            return remoteData;
        }
    }
}
