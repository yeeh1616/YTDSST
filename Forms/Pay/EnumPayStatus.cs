using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.Pay
{
    /**
     * 微信支付状态
     */ 
    enum EnumPayStatus
    {
        //初始状态
        PAY_INIT = -1,
        //不支持在线支付
        PAY_NOT_SUPPPRT = 0,
        //正在生成二维码
        PAY_GENERATE_QRCODE = 1,
        //等待用户扫码
        PAY_WAIT_SCAN_QRCODE = 2,       
        //支付成功
        PAY_SUCCESS = 3, 
        //支付失败
        PAY_FAILED = 4
    }
}
