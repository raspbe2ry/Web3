using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DOL;
using Web3.Helpers.HighChart;

namespace Web3.Controllers
{
    public class AnalyticsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMonthlyCosts(DateTime? filterDate, string period)
        {
            HighChartData data = null;
            List<string> cat = null;

            if (period.Equals("year"))
            {
                data = new MonthlyCostByCategory().GetForYear(filterDate.HasValue ? filterDate.Value.Year : DateTime.Now.Year);
                cat = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            }
            else if(period.Equals("month"))
            {
                data = new MonthlyCostByCategory().GetForMonth(filterDate);
                cat = GenerateListForMonth(filterDate);
            }

            return Json(new { data , cat});
        }

        private List<string> GenerateListForMonth(DateTime? date)
        {
            List<string> cat = new List<string>();

            var month = date.HasValue ? date.Value.Month : DateTime.Now.Month;
            var year = date.HasValue ? date.Value.Year : DateTime.Now.Year;
    
            if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                for (int i = 1; i <= 31; i++)
                    cat.Add(i.ToString());
            }
            else if(month == 4 || month == 6 || month == 9 || month == 11)
            {
                for (int i = 1; i <= 30; i++)
                    cat.Add(i.ToString());
            }
            else if(year % 4 == 0)
            {
                for (int i = 1; i <= 29; i++)
                    cat.Add(i.ToString());
            }
            else
            {
                for (int i = 1; i <= 28; i++)
                    cat.Add(i.ToString());
            }

            return cat;
        }
    }
}