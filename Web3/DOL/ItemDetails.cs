using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.DL;
using Web3.Models;

namespace Web3.DOL
{
    public class ItemDetails
    {
        private static GenericRepository<Item> itemRepo = new GenericRepository<Item>(new ApplicationDbContext());
        private static GenericRepository<Catalog> catalogRepo = new GenericRepository<Catalog>(new ApplicationDbContext());

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal OneInstancePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Qty { get; set; }
        public decimal Discount { get; set; }
        public string ItemCode { get; set; }
        public decimal TotalPrice { get; set; }
        
        public string CategoryCode { get; set; }

        public static ItemDetails GetRelacement(int itemId)
        {
            var data = itemRepo.Get(x => x.Id == itemId, null, "Catalog").FirstOrDefault();

            if (data != null)
            {
                return new ItemDetails()
                {
                    ItemId = data.Id,
                    Discount = data.Catalog.Discount,
                    ItemCode = data.Code,
                    OneInstancePrice = data.Price,
                    DiscountedPrice = data.Price * (1 - data.Catalog.Discount * (decimal)0.01),
                    CategoryCode = data.CategoryCode
                };
            }
            else
                return null;
        }
    }
}