using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.BLL;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Model.Param;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class UserInfoController : BaseController
    {
        short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
        //
        // GET: /UserInfo/
        //UserInfoService UserInfoService =new UserInfoService();//Spring.Net
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }

        #region 获取用户

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAllUserInfos()
        {
            //jquery easyui: table: {total:32,rows:[{},{}]}

            //easyui: table在初始化时候自动发送以下两个参数值。
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            //过滤的用户名   过滤备注schName   schRemark
            string schName = Request["schName"];
            string schRemark = Request["schRemark"];

            

            var queryParam= new UserQueryParam()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = 0,
                    SchName = schName,
                    SchRemark = schRemark
                };

            var pageData = UserInfoService.LoagPageData(queryParam);

            var temp = pageData.Select(u => new
            {
                ID = u.ID,
                u.UName,
                u.Remark,
                u.ShowName,
                u.SubTime,
                u.ModfiedOn,
                u.Pwd
            });


            //拿到当前页的数据
            //var pageData = UserInfoService.GetPageEntities(pageSize, pageIndex, out total,
            //                                                u => u.DelFlag == delflagNormal, u => u.ID, true)
            //.select(
            //      u =>
            //      new
            //      {
            //          id = u.id,
            //          u.uname,
            //          u.remark,
            //          u.showname,
            //          u.subtime,
            //          u.modfiedon,
            //          u.pwd
            //      });


            var data = new { total = queryParam.Total, rows = temp.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 添加用户
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.ModfiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            userInfo.DelFlag = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);

            return Content("ok");
        }
        #endregion


        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");
        }
        #endregion

        #region 删除
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除数据！");
            }

            //正常处理
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strIds)
            {
                idList.Add(int.Parse(strId));
            }
            //UserInfoService.DeleteList(idList);
            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");

        }
        #endregion


        #region create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
        #endregion


        #region 设置角色
        public ActionResult SetRole(int id)
        {
            //当前要设置角色的用户
            int userId = id;
            
            UserInfo  user= UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();

            //把所有的角色发送 到前台
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList();

            //用户已经关联的角色发送到前台。
            ViewBag.ExitsRoles = (from r in user.RoleInfo
                                  select r.ID).ToList();

            return View(user);

        }

        //给用户设置角色
        public ActionResult ProcessSetRole(int UId)
        {
            //第一：当前用户id  --uid
            //第二：所有打上对勾的 角色。 ---> list
            List<int> setRoleIdList =new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }

            UserInfoService.SetRole(UId, setRoleIdList);
            return Content("ok");

        }

        #endregion

        #region 设置特殊权限
        public ActionResult SetAction(int id)
        {
            ViewBag.User = UserInfoService.GetEntities(u => u.ID == id).FirstOrDefault();

            ViewData.Model = ActionInfoService.GetEntities(a => a.DelFlag == delflagNormal).ToList();

            return View();
        }

        //做一个删除  特殊权限。
        public ActionResult DeleteUserAction(int UId, int ActionId)
        {

            var rUserAction= R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoID == ActionId && r.UserInfoID == UId)
                                        .FirstOrDefault();
            if (rUserAction != null)
            {
                //rUserAction.DelFlag = (short) Heima8.OA.Model.Enum.DelFlagEnum.Deleted;
                R_UserInfo_ActionInfoService.DeleteListByLogical(new List<int>() {rUserAction.ID});
            }
            return Content("ok");
        }


        //设置当前用户的特殊权限
        public ActionResult SetUserAction(int UId, int ActionId, int Value)
        {
            //{UId:uId,ActionId:actionId,Value:value}
              var rUserAction= R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoID == ActionId && r.UserInfoID == UId && r.DelFlag==delflagNormal)
                                        .FirstOrDefault();
            if (rUserAction != null)
            {
                rUserAction.HasPermission = Value == 1 ? true : false;
                R_UserInfo_ActionInfoService.Update(rUserAction);
            }
            else
            {
                R_UserInfo_ActionInfo rUserInfoActionInfo =new R_UserInfo_ActionInfo();
                rUserInfoActionInfo.ActionInfoID = ActionId;
                rUserInfoActionInfo.UserInfoID = UId;
                rUserInfoActionInfo.HasPermission = Value == 1 ? true : false;
                rUserInfoActionInfo.DelFlag = delflagNormal;
                R_UserInfo_ActionInfoService.Add(rUserInfoActionInfo);
            }

            return Content("ok");
        }

        #endregion
    }
}
