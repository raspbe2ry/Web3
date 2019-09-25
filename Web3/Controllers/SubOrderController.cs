using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DL;
using Web3.DTO;
using Web3.Helpers;

namespace Web3.Controllers
{
    public class SubOrderController : Controller
    {
        public JsonResult GetSubOrderData(int? orderId, DataTableFilter dtFilter)
        {
            var data = DTOSubOrder.MapToDTO(new SubOrderRepository().GetData(dtFilter, orderId));

            return Json(new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = data.Data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}