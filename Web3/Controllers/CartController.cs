using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web3.DL;
using Web3.DOL;

namespace Web3.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            var model = new List<ItemCategoryDetails>();

            if (TempData["CartHasItems"] != null)
            {
                Cart currentCart = (Cart)TempData["CurrentCart"];
                model = currentCart.GetItemsByCategory();
            }

            ViewBag.TotalAmount = model.Sum(x => x.TotalPrice);

            return View(model);
        }

        public JsonResult AddEntityToCart(int itemId, int qty)
        {
            int differentItemsInCart = -1;

            if (TempData["CartHasItems"] == null)
            {
                Cart cart = new Cart();
                cart.EntityList.Add(new CartEntity()
                {
                    ItemId = itemId,
                    Qty = qty
                });

                TempData["CartHasItems"] = true;
                TempData["CurrentCart"] = cart;

                differentItemsInCart = cart.EntityList.Count();
            }
            else
            {
                Cart currentCart = (Cart)TempData["CurrentCart"];

                int index = currentCart.EntityList.FindIndex(x => x.ItemId == itemId);
                if (index >= 0)
                {
                    currentCart.EntityList[index].Qty += qty;
                }
                else
                {
                    currentCart.EntityList.Add(new CartEntity
                    {
                        ItemId = itemId,
                        Qty = qty
                    });
                }

                TempData["CurrentCart"] = currentCart;
                TempData["CartHasItems"] = true;

                differentItemsInCart = currentCart.EntityList.Count();
            }

            return Json(differentItemsInCart);
        }

        public JsonResult CheckCartState()
        {
            if (TempData["CurrentCart"] != null)
            {
                Cart currentCart = (Cart)TempData["CurrentCart"];
                int differentItemsInCart = currentCart.EntityList.Count();
                return Json(differentItemsInCart);
            }
            else
            {
                return Json(0);
            }
        }

        public JsonResult SaveCart(List<CartEntity> items)
        {
            try
            {
                Cart newCart = new Cart();
                foreach (var item in items)
                {
                    if (item.Qty > 0)
                    {
                        newCart.EntityList.Add(new CartEntity
                        {
                            ItemId = item.ItemId,
                            Qty = item.Qty
                        });
                    }
                }

                TempData["CurrentCart"] = newCart;
                TempData["CartHasItems"] = true;
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public PartialViewResult AnalyzeCart()
        {
            if (TempData["CartHasItems"] != null)
            {
                Cart currentCart = (Cart)TempData["CurrentCart"];

                List<int> itemIds = currentCart.EntityList.Select(x => x.ItemId).ToList();
                List<BestItemOffer> bestOffers = BestItemOffer.MapToBests(itemIds);

                return PartialView("_AnalyzedCartPartial", bestOffers);
            }

            return null;
        }

        public JsonResult ConvertItem(BestItemOffer conversionItem)
        {
            if (TempData["CartHasItems"] != null)
            {
                Cart currentCart = (Cart)TempData["CurrentCart"];
                var index = currentCart.EntityList.FindIndex(x => x.ItemId == conversionItem.ItemId);

                if (index >= 0)
                    currentCart.EntityList[index].ItemId = conversionItem.BestItemId;

                TempData["CurrentCart"] = currentCart;

                var replacement = ItemDetails.GetRelacement(conversionItem.BestItemId);

                return Json(new { oldId = conversionItem.ItemId, replacement, success = true });
            }

            return Json(new { success = false });
        }

    }
}