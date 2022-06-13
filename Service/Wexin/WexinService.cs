using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YTDSSTGenII.Utils.Network.Http;
using YTDSSTGenII.Utils.Network;

namespace YTDSSTGenII.Service.Wexin
{
    public class WexinService
    {      
        public delegate void WexinRequestDeletage(ResponseData data);

        public WexinRequestDeletage WexinRequestDeletageMethod { set; get; }

        public void loadQRCode()
        {
            Dictionary<String, String> requestParams = new Dictionary<string, string>();

            //ResponseData responseData = HttpClientProxy.newProxy().postRequest<QRData>("/", requestParams);

            if (this.WexinRequestDeletageMethod != null)
            {
                //this.WexinRequestDeletageMethod(responseData);
            }
        }
    }

    public enum WEXIN_NET_PROTOCOL { 
        /**
         * 初始化二维码
         */
        INIT_QRCODE = 0x01,
        /**
         * 同步销售结果
         */ 
        SYNC_SELL_RESULT = 0x02
    }
}
