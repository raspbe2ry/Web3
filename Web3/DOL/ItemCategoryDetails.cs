using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.DOL
{
    public class ItemCategoryDetails
    {
        public string CategoryCode { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice { get; set; }

        public List<ItemDetails> ItemDetails { get; set; }
    }
}