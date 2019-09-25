using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.Helpers.Filters
{
    public class OrderFilter
    {
        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}