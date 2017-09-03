using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class OrderInfoController : BaseController
    {
        //
        // GET: /OrderInfo/
        public IOrderInfoService OrderInfoService { get; set; }

        public ActionResult Index()
        {
            if (Session["loginUser"] == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }

            ViewData.Model = OrderInfoService.GetEntities(u => u.DelFlag == DeleteFlag.DelflagNormal).ToList();
            return View();
        }

    }
}
