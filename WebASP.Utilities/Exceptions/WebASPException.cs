using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP.Utilities.Exceptions
{
    public class WebASPException:Exception
    {
        public WebASPException()
        {

        }
        public WebASPException(string message):base(message)
        {

        }

        public WebASPException(string message,Exception inner):base(message,inner)
        {

        }
    }
}
