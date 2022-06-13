using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.Pay
{
    /**
     * 机头出票的状态
     */ 
    enum EnumCutPaperStatus
    {
        //未出票，初始状态
        CUT_PAPER_INIT = 0,
        //出票成功
        CUT_PAPER_SUCCESS = 1,
        //出票失败
        CUT_PAPER_FAILED = 2,
        //出票部分失败
        CUT_PAPER_PART_FAILED = 3,
        //正在出票
        CUT_PAPER_ING = 4
    }
}
