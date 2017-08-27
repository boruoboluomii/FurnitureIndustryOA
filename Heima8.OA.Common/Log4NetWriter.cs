using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Heima8.OA.Common
{
    public class Log4NetWriter:ILogWriter
    {

        public void WriteLogInfo(string txt)
        {
            ILog logWriete = log4net.LogManager.GetLogger("Demo");
            logWriete.Error(txt);
        }
    }
}
