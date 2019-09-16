using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.DOL
{
    public class CartEntity
    {
        public int ItemId { get; set; }
        public string ItemCategory { get; set; }
        public decimal ItemPrice { get; set; }
        public int Qty { get; set; }
    }
}