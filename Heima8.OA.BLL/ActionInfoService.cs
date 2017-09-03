using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
    //模块内高内聚。模块间低耦合。

    //变化点：1、跨数据库。有mysql，slqserver2、数据库访问驱动层驱动变化。

    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService //crud
    {


        public bool SetRole(int userId, List<int> roleIds)
        {
            var actionInfo = DbSession.ActionInfoDal.GetEntities(u => u.ID == userId && u.DelFlag == DeleteFlag.DelflagNormal).FirstOrDefault();
            actionInfo.RoleInfo.Clear();//全剁掉。

            var allRoles = DbSession.RoleInfoDal.GetEntities(r =>r.DelFlag == DeleteFlag.DelflagNormal && roleIds.Contains(r.ID));
            foreach (var roleInfo in allRoles)
            {
                actionInfo.RoleInfo.Add(roleInfo);//加最新的角色。
            }
            DbSession.SaveChanges();
            return true;
        }
    }
}
