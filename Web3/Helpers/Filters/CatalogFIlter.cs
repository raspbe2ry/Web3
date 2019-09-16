using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.Helpers.Filters
{
    public class CatalogFilter
    {
        public DateTime? BeginingDateFrom { get; set; }
        public DateTime? BeginingDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public int? VendorId { get; set; }
    }
}