using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
        //
        // GET: /Home/
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            if(LoginUser == null) 
//            return View("/UserLogin/Index");
//            return RedirectToAction("UserLogin", "Index");  
            return Redirect("UserLogin/Index");  
            ViewBag.AllMenu= LoadUserMenu();

            //最新的主框架
            return View();
            //老式的主框架 方式。
            //return View("TreeIndex");
        }

        //根据用户的权限加载菜单数据
        public List<ActionInfo> LoadUserMenu()
        {
            //拿到当前用户
            int userId = this.LoginUser.ID;
            var user = UserInfoService.GetEntities(u => u.ID == userId).FirstOrDefault();
            //拿到当前用户所有的权限【必须是菜单类型权限。】
            var allRole = user.RoleInfo;

            //把用户对应的所有的角色关联权限的id都拿出来了。
            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.ID).ToList();
            //用户直接拒绝的权限。
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoID).ToList();

            //var allActionIds = allRoleActionIds.Where(u => !allDenyActionIds.Contains(u));
            //角色权限 -  特殊拒绝权限
            var allActionIds = (from a in allRoleActionIds
                                where !allDenyActionIds.Contains(a)
                                select a).ToList();


            //特殊直接允许权限
            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                    where t.HasPermission == true
                                    select t.ActionInfoID).ToList();
            
            //把当前用户的所有的权限拿到。
            allActionIds.AddRange(allUserActionIds.AsEnumerable());
            allActionIds = allActionIds.Distinct().ToList();//去重操作。


            var actionList =
                ActionInfoService.GetEntities(
                    a => allActionIds.Contains(a.ID) && a.IsMenu == true && a.DelFlag == delflagNormal)
                    .OrderBy(a => a.Sort)
                    .ToList();

            //{ icon: '/Content/Images/Home/3DSMAX.png', title: '用户列表', url: '/UserInfo/Index' }
            //var data = from a in actionList
            //           select new { icon =a.MenuIcon,title=a.ActionName,url=a.Url};
            //return Json(data, JsonRequestBehavior.AllowGet);

            return actionList;

        }

    }
}
