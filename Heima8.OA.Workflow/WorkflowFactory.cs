using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heima8.OA.Workflow
{
    public class WorkflowFactory
    {
       public static Activity GetActivity(string s)
        {
            if (s == null) return null;
            if (s.Contains("家具") || s.ToLower().Contains("productflow"))
            {
                return new ProductFlow();
            }
            return null;
        }
    }
}
