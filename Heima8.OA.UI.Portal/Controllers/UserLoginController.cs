using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.Common;
using Heima8.OA.IBLL;
using Heima8.OA.UI.Portal.Models;

namespace Heima8.OA.UI.Portal.Controllers
{
    [LoginCheckFilter(IsCheck = false)]
    public class UserLoginController : Controller
    {
        //
        // GET: /UserLogin/
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region 验证码

        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);

            //验证码放到Session
            Session["VCode"] = strCode;

            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

            return File(imgBytes, @"image/jpeg");

        }
        #endregion

        
        bool isNeedVcode = false;
        #region 处理登录的表单
        public ActionResult ProcessLogin()
        {
            //第一步：处理验证码。
            //拿到表单中的验证码
            #region 验证码
#if !DEBUG
            string strCode = Request["vCode"];

            //拿到Session中的验证码
            string sessionCode = Session["VCode"] as string;

            Session["VCode"] = null;
            if (string.IsNullOrEmpty(sessionCode))
            {
                return Content("验证码错误");
            }

            if (strCode != sessionCode)
            {
                return Content("验证码错误！");
            } 
#endif

            #endregion

            //第二步：处理验证4用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
            short delNormal =(short) Heima8.OA.Model.Enum.DelFlagEnum.Normal;
            var userInfo =
                UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal)
                               .FirstOrDefault();

            if (userInfo == null)//没有查询出数据来。
            {
                return Content("用户名密码错误！会登录吗？");
            }

            //Session["loginUser"] = userInfo;
            //用memcache+cookie代替之。
            //立即分配一个标志，Guid。把标志作为 mm存储数据的key，把用户对象放到 mm。 把guid写到客户端cookie里面去。
            string userLoginId = Guid.NewGuid().ToString();

            //把用户的数据写到mm,怎么解决变化：写到不同地方去，可能同时写入多个地方。
            Common.Cache.CacheHelper.AddCache(userLoginId,userInfo,DateTime.Now.AddMinutes(20));

            //往客户端写入cookie
            Response.Cookies["userLoginId"].Value = userLoginId;

            //如果正确那么跳转到首页。
            return Content("ok");
        }
#endregion

    }
}
