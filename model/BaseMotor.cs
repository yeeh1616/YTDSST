using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTDSSTGenII.Forms.model
{
   public abstract class BaseMotor
    {
        private String motorId;
        private lottery lottery;//彩票对象
        public lottery Lottery
        {
            get { return lottery; }
            set { lottery = value; }
        }

        public string MotorId
        {
            get
            {
                return motorId;
            }

            set
            {
                motorId = value;
            }
        }
    }
}
