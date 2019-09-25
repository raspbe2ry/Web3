using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DL;
using Web3.DTO;
using Web3.Helpers;
using Web3.Helpers.Filters;

namespace Web3.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCatalogData(DataTableFilter dtFilter, CatalogFilter filter)
        {
            var data = DTOCatalog.MapToDTO(new CatalogRepository().GetData(dtFilter, filter));

            return Json(new {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = data.Data
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCatalog(int id)
        {
            var data = DTOCatalog.MapToDTO(new CatalogRepository().GetIndividual(id));

            return Json(data);
        }
    }
}