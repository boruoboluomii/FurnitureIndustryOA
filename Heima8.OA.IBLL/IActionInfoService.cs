using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.Model;

namespace Heima8.OA.IBLL
{
    public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
       

        bool SetRole(int userId, List<int> roleIds);
    }
}
