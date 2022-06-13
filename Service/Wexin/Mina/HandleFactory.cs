using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Service.Wexin.Mina.Handler;

namespace YTDSSTGenII.Service.Wexin.Mina
{
    class ProtocolHandleFactory
    {
        private static Dictionary<String, IProtocolHandle> handlers = new Dictionary<string,IProtocolHandle>();

        static ProtocolHandleFactory()
        {
            //注册协议处理器
            handlers.Add("C001", new C001_Handle());
            handlers.Add("C002", new C002_Handle());
            handlers.Add("C003", new C003_Handle());
            handlers.Add("C004", new C004_Handle());
            handlers.Add("C006", new C006_Handle());
            handlers.Add("C007", new C007_Handle());
            handlers.Add("C008", new C008_Handle());
            handlers.Add("C009", new C009_Handle());
        }

        public static IProtocolHandle Lookup(String protocolId) {
            IProtocolHandle handler = null;
            bool success = handlers.TryGetValue(protocolId, out handler);

            if (success)
                return handler;
            else
                return null;
        }
    }
}
