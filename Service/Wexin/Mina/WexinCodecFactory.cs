using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mina.Filter.Codec;
using Mina.Core.Session;

namespace YTDSSTGenII.Service.Wexin.Mina
{
    public class WexinCodecFactory : IProtocolCodecFactory 
    {
        private IProtocolDecoder decoder;
        private IProtocolEncoder encoder;

        public WexinCodecFactory() 
        {
            this.decoder = new WexinDecoder();
            this.encoder = new WexinEncoder();
        }

        public IProtocolDecoder GetDecoder(IoSession session) {
            return decoder;
        }

        public IProtocolEncoder GetEncoder(IoSession session)
        {
            return encoder;
        }
    }
}
