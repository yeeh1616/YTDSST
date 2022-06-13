using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Service
{
    public class MotorController
    {
        public delegate void MotorControllerDelegate(CutPaperResult result);

        private bool initialized = false;
        private LinkedList<Motor> motos = new LinkedList<Motor>();

        public void InitMotoStatus() 
        {
            //TODO 按顺序加载马达信息
            initialized = true;
        }

        public bool CutPaper(int motorNo, MotorControllerDelegate callback) {
            if (!initialized)
                return false;

            //TODO 出票
            //启动线程或者Timer开始出票

            //出票结束或者异常，通过回调接口通知被调用层，出票异常后，禁用相关的马达
           
            return true;
        }

        public void CheckMotorStatus() { 
        
        }

        public void GetMotoSnapshot() 
        {
 
        }
    }

    public class CutPaperResult {
        public CutPaperResult() { 
        }

        public bool Result { get; set; }
        public int Number { get; set; }
    }

    public class Motor {

    }
}
