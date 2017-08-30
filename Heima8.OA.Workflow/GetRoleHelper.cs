using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.BLL;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.Workflow
{
    public class GetRoleHelper
    {
        public static RoleInfo GetRole(string keyString)
        {
            short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
            IApplicationContext ctx = ContextRegistry.GetContext();
            var roleInfoService = ctx.GetObject("IRoleInfoService") as IRoleInfoService;
            if (roleInfoService != null)
            {
                var role = roleInfoService.GetEntities(r => r.DelFlag == delflagNormal && r.RoleName.ToLower().Contains(keyString.ToLower())).FirstOrDefault();
                if (role != null) return role;
            }
            return null;
        }

        static public string GetFlowToRoleKeyWord(RoleInfo roleInfo, int tempId)
        {
            if (roleInfo.RoleName.Contains("销售"))
            {
                return "设计";
            }
            if (roleInfo.RoleName.Contains("设计"))
            {
                short normalFlag = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;

                IApplicationContext ctx = ContextRegistry.GetContext();
                var wF_TempService = ctx.GetObject("WF_TempService") as WF_TempService;
                var temp = wF_TempService.GetEntities(t => t.DelFlag == normalFlag && t.ID == tempId).FirstOrDefault();
                if (temp != null && "小锯".Contains(temp.Remark))
                {
                    return "小锯";
                }
                else if (temp != null && "数控".Contains(temp.Remark))
                {
                    return "数控";
                }
                else
                {
                    //todo 需要验证

                }
                return null;
            }
            if (roleInfo.RoleName.Contains("小锯"))
            {
                return "封边";
            }
            if (roleInfo.RoleName.Contains("数控"))
            {
                return "封边";
            }
            if (roleInfo.RoleName.Contains("封边"))
            {
                return "排钻";
            }
            if (roleInfo.RoleName.Contains("排钻"))
            {
                return "包装";
            }
            if (roleInfo.RoleName.Contains("包装"))
            {
                return "包装";
            }
            return null;
        }
    }
}
