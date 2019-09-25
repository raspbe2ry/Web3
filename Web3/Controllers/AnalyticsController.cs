using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DOL;

namespace Web3.Controllers
{
    public class AnalyticsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMonthlyCosts(int year)
        {
            var data = new MonthlyCostByCategory().GetForYear(2019);

            //List<double> data = new List<double>() { 1.1, 2.2, 3.3, 4.4, 1.1, 2.2, 3.3, 4.4, 1.1, 2.2, 3.3, 4.4};
            return Json(new { data });
        }
    }
}