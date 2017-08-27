using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{

    public class WFTempController : Controller
    {
        //
        // GET: /WFTemp/

        public IWF_TempService WF_TempService { get; set; }
        short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }
        #region 加载数据

        public ActionResult GetAllTempInfos()
        {

            //easyui: table在初始化时候自动发送以下两个参数值。
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = WF_TempService.GetPageEntities(pageSize, pageIndex, out total,
                                              u => u.DelFlag == delflagNormal, u => u.ID, true);

            var tempData =
                temp.Select(
                    a =>
                    new
                    {
                        a.ID,
                        a.TepName,
                        a.Remark,
                        a.SubTime,
                        a.ActityType,
                        a.DelFlag
                    });

            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 添加

        //显示添加页面
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WF_Temp temp)
        {
            temp.DelFlag = (short) Heima8.OA.Model.Enum.DelFlagEnum.Normal;
            temp.SubTime = DateTime.Now;
            WF_TempService.Add(temp);

            if (temp.Remark.Contains("小锯"))
            {
                Heima8.OA.Workflow.GetRoleHelper.TypeDictionary.Add(temp.ID, "小锯");
            }else if (temp.Remark.Contains("数控"))
            {
                Heima8.OA.Workflow.GetRoleHelper.TypeDictionary.Add(temp.ID, "数控");
            }
            else
            {
                //todo 需要验证

            }
            return Content("ok");
        }
        #endregion

        #region 发起流程的index页面
        public ActionResult StartWF()
        {

            ViewData.Model = WF_TempService.GetEntities(u => true).ToList();

            return View();
        }
        #endregion
    }
}
