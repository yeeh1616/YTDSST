using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Http;

namespace YTDSSTGenII.Utils.Network.Http
{
    public class ResponseData
    {
        public ResponseStatus Status { get; set; }
        public HttpStatusCode HttpStatusCode { get; set;}
        public Object Data { get; set; }
        public String Message { get; set; }
    }

    public enum ResponseStatus 
    { 
        CANCEL = 1001,
        FAULT = 1002,
        SUCCESS = 200
    }
}
