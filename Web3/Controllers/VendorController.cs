using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DL;

namespace Web3.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetVendor(int? id)
        {
            return View();
        }

        public JsonResult GetVendorSelectList(string searchTerm, int pageSize, int pageNumber)
        {
            var result = new VendorRepository().GetData(searchTerm, pageSize, pageNumber);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}