using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.DL;
using Web3.Models;

namespace Web3.DOL
{
    public class Cart
    {
        private static GenericRepository<Item> itemRepo = new GenericRepository<Item>(new ApplicationDbContext());
        private static GenericRepository<Catalog> catalogRepo = new GenericRepository<Catalog>(new ApplicationDbContext());

        public List<CartEntity> EntityList { get; set; }
        public decimal TotalPrice { get; set; }

        public Cart()
        {
            EntityList = new List<CartEntity>();
            TotalPrice = 0;
        }

        public List<ItemCategoryDetails> GetItemsByCategory()
        {
            var data = (from el in EntityList
                        join i in itemRepo.Get(null, null, "") on el.ItemId equals i.Id
                        join c in catalogRepo.Get(null, null, "") on i.CatalogId equals c.Id
                        select new ItemDetails
                        {
                            ItemId = i.Id,
                            Qty = el.Qty,
                            OneInstancePrice = i.Price,
                            DiscountedPrice = i.Price * (1 - c.Discount * (decimal)0.01),
                            TotalPrice = i.Price * el.Qty * (1 - c.Discount* (decimal)0.01),
                            Discount = c.Discount,
                            ItemCode = i.Code,
                            ItemName = i.Name,
                            CategoryCode = i.CategoryCode
                        } into itemWithCat
                        group itemWithCat by itemWithCat.CategoryCode into res
                        select new ItemCategoryDetails()
                        {
                            CategoryCode = res.Key,
                            Qty = res.Sum(x => x.Qty),
                            TotalPrice = res.Sum(x => x.TotalPrice),
                            ItemDetails = res.ToList()
                        }).ToList();

            return data;

        }
    }
}

