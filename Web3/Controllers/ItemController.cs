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
    public class ItemController : Controller
    {
        public JsonResult GetItemData(DataTableFilter dtFilter, ItemFilter filter)
        {
            var data = DTOItem.MapToDTO(new ItemRepository().GetData(dtFilter, filter));

            return Json(new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = data.Data
            }, JsonRequestBehavior.AllowGet);

        }
    }
}