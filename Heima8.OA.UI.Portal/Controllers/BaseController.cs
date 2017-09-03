using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.Common.Cache;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        //在当前的控制器里面所有的方法执行之前。都先执行此代码

        public bool IsCheckUserLogin = true;
        public UserInfo LoginUser { get; set; }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //mvc请求来了之后，根据请求地址，创建控制器工厂(Spring.Net)，控制器工厂创建控制器，执行方法。
            //Spring.Net

            base.OnActionExecuting(filterContext);

            var items = filterContext.RouteData.Values;



            if (IsCheckUserLogin)
            {
                //使用mm+cookie代替session
                //校验用户是否已经登录

                //从缓存中拿到当前的登录的用户信息。
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，。超时。
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                LoginUser = userInfo;
                //滑动窗口机制。
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));


                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo; 
                //}


                //校验权限
                //把当前请求对应的权限数据拿到。
                if (LoginUser.UName == "admin")
                {
                    return;//侯梦
                }

                string url = Request.Url.AbsolutePath.ToLower();
                string httpMethod = Request.HttpMethod.ToLower();
                //默认都有流程的操作了
                if(url.Contains("WFInstance".ToLower())) return;

                bool isGetMethodWithParameter = url.Count(ch => ch == '/') != 2;
                if (isGetMethodWithParameter)
                {
                    int lastIndex = url.LastIndexOf('/');
                    url = url.Substring(0, lastIndex);
                }
                //通过容器创建一个对象。
                IApplicationContext ctx = ContextRegistry.GetContext();

                IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;

                IR_UserInfo_ActionInfoService rUserInfoActionInfoService =
                    ctx.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;

                IUserInfoService UserInfoService =
                  ctx.GetObject("UserInfoService") as IUserInfoService;


                var actionInfo =//拿到当前请求对应的权限数据
                actionInfoService.GetEntities(a => a.DelFlag == DeleteFlag.DelflagNormal && a.Url.ToLower() == url && a.HttpMethd.ToLower() == httpMethod)
                                 .FirstOrDefault();

                if (actionInfo == null)
                {
                    Response.Redirect("/Error.html");
                }


                //一号线 
                var rUAs = rUserInfoActionInfoService.GetEntities(u => u.DelFlag == DeleteFlag.DelflagNormal && u.UserInfoID == LoginUser.ID);

                var item = (from a in rUAs
                            where a.ActionInfoID == actionInfo.ID
                           select a).FirstOrDefault();
                if (item != null)
                {
                    if (item.HasPermission == true)
                    {
                        return;
                    }
                    else
                    {
                        Response.Redirect("/Error.html");
                    }
                }


                //2号
                var user = UserInfoService.GetEntities(u => u.DelFlag == DeleteFlag.DelflagNormal && u.ID == LoginUser.ID).FirstOrDefault();
                //拿到所有的角色
                var allRoles = from r in user.RoleInfo
                               select r;
                //通过角色拿到所有的权限
                var actions = from r in allRoles
                             from a in r.ActionInfo
                             select a;
                //看当前权限是否在  角色对应权限集合中。
                var temp = (from a in actions
                            where a.ID == actionInfo.ID
                            select a).Count();
                if (temp <= 0)
                {
                    Response.Redirect("/Error.html");
                }
            }
        }
    }
}