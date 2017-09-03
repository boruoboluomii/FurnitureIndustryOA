using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        //
        // GET: /RoleInfo/
        short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
        public IRoleInfoService RoleInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region load roleinfos
        public ActionResult GetAllRoleInfos()
        {
            //easyui: table在初始化时候自动发送以下两个参数值。
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = RoleInfoService.GetPageEntities(pageSize, pageIndex, out total,
                                              u => u.DelFlag == delflagNormal, u => u.ID, true);

            var tempData =
                temp.Select(
                    a =>
                    new
                    {
                        a.ID,
                        a.RoleName,
                        a.Remark,
                        a.SubTime,
                        a.ModfiedOn,
                        a.DelFlag

                    });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region Add
        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = delflagNormal;
            roleInfo.ModfiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            RoleInfoService.Add(roleInfo);
            return Content("ok");
        }
        #endregion
        #region 修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = RoleInfoService.GetEntities(u => u.DelFlag == DeleteFlag.DelflagNormal && u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            RoleInfoService.Update(roleInfo);
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
            RoleInfoService.DeleteListByLogical(idList);
            return Content("ok");

        }
        #endregion
    }
}
