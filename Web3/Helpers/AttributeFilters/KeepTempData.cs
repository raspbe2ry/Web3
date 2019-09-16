using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web3.Helpers.AttributeFilters
{
    public class KeepTempDataAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.TempData.Keep("CartHasItems");
            filterContext.Controller.TempData.Keep("CurrentCart");
        }
    }
}