using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.DOL
{
    public class Cart
    {
        public List<CartEntity> EntityList { get; set; }
        public decimal TotalPrice { get; set; }

        public Cart()
        {
            EntityList = new List<CartEntity>();
            TotalPrice = 0;
        }
    }
}