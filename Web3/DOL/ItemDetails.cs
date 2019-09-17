using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.DOL
{
    public class ItemDetails
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal OneInstancePrice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
        public string ItemCode { get; set; }
        public decimal TotalPrice { get; set; }
        
        public string CategoryCode { get; set; }
    }
}