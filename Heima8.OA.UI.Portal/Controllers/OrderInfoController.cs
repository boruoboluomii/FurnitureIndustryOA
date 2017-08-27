using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;

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

            ViewData.Model = OrderInfoService.GetEntities(u => true).ToList();
            return View();
        }

    }
}
