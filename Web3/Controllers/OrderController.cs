using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DL;
using Web3.DOL;
using Web3.DTO;
using Web3.Helpers;
using Web3.Helpers.Filters;
using Web3.Models;

namespace Web3.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetOrderData(DataTableFilter dtFilter, OrderFilter filter)
        {
            var data = DTOOrder.MapToDTO(OrderRepository.GetData(dtFilter, filter));

            return Json(new
            {
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = data.Data
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PerformOrder()
        {
            try
            {
                if (TempData["CartHasItems"] != null)
                {
                    Cart currentCart = (Cart)TempData["CurrentCart"];

                    List<DTOOrderItem> orderItems = currentCart.EntityList.Select(x => new DTOOrderItem()
                    {
                        ItemId = x.ItemId,
                        Qty = x.Qty
                    }).ToList();

                    int orderId = OrderRepository.CreateOrder();

                    List<int> subOrderIds = SubOrderRepository.Create(orderItems, orderId);

                    List<int> orderItemIds = OrderItemRepository.CreateOrderItems(orderItems, orderId);
                }
                else
                {
                    return Json(new { success = false, message="Cart is empty." });
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false });
            }

            TempData["CartHasItems"] = null;
            TempData["CurrentCart"] = null;
            return Json(new { success = true });
        }
    }
}