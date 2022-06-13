using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Service.Wexin.Data;


namespace YTDSSTGenII.Service.Wexin.Mina
{
    public interface IProtocolHandle
    {
        RemoteData DoHandle(WexinPacket packet);
    }
}
