using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace YTDSSTGenII.Service.Exception
{
    public class ServiceException  : System.Exception
    {
        public ServiceException() : base()
        {
        }

        public ServiceException(String msg)
            : base(msg)
        { 
        }

        public ServiceException(String msg, System.Exception exp) : base(msg, exp)
        {

        }
    }
}
