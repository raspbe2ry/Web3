using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.DL;
using Web3.Models;

namespace Web3.DOL
{
    public class BestItemOffer
    {
        private static GenericRepository<Item> itemRepo = new GenericRepository<Item>(new ApplicationDbContext());
        private static GenericRepository<Catalog> catalogRepo = new GenericRepository<Catalog>(new ApplicationDbContext());
        private static GenericRepository<Vendor> vendorRepo = new GenericRepository<Vendor>(new ApplicationDbContext());

        public int ItemId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal OriginalDiscount { get; set; }
        public decimal OriginalPriceWithDiscount { get; set; }
        public int? OriginalVendorId { get; set; }
        public string OriginalVendorName { get; set; }
        public int OriginalCatalogId { get; set; }
        public string OriginalCatalogCode { get; set; }

        public int BestItemId { get; set; }
        public decimal BestPrice { get; set; }
        public decimal BestDiscount { get; set; }
        public decimal BestPriceWithDiscount { get; set; }
        public int? BestVendorId { get; set; }
        public string BestVendorName { get; set; }
        public int BestCatalogId { get; set; }
        public string BestCatalogCode { get; set; }

        public string CategoryCode { get; set; }
        public int CatalogId { get; set; }

        public string ItemCode { get; set; }
        public string BestItemCode { get; set; }

        public static BestItemOffer MapToBest(int itemId)
        {
            var best = (from i in itemRepo.Get(x => x.Id == itemId, null, "")
                        join c in catalogRepo.Get(x => true, null, "Vendor") on i.CatalogId equals c.Id
                        join ib in itemRepo.Get(x => true, null, "") on i.CategoryCode equals ib.CategoryCode
                        join cb in catalogRepo.Get(x => true, null, "Vendor") on ib.CatalogId equals cb.Id
                        select new BestItemOffer
                        {
                            ItemId = itemId,
                            BestItemId = ib.Id,
                            OriginalPrice = i.Price,
                            OriginalDiscount = c.Discount,
                            OriginalPriceWithDiscount = i.Price * (1 - (decimal)0.01 * c.Discount),
                            BestDiscount = cb.Discount,
                            BestPrice = ib.Price,
                            BestPriceWithDiscount = ib.Price * (1 - (decimal)0.01 * cb.Discount),
                            CategoryCode = i.CategoryCode,
                            CatalogId = cb.Id,
                            OriginalCatalogCode = c.Code,
                            OriginalCatalogId = c.Id,
                            OriginalVendorId = c.VendorId,
                            OriginalVendorName = c.Vendor.Name,
                            BestCatalogCode = cb.Code,
                            BestCatalogId = cb.Id,
                            BestVendorId = cb.VendorId,
                            BestVendorName = cb.Vendor.Name,
                            ItemCode = i.Code,
                            BestItemCode = ib.Code
                        }).OrderBy(x=>x.BestPriceWithDiscount).FirstOrDefault();


                return best;
        }

        public static List<BestItemOffer> MapToBests(List<int> itemIds)
        {
            var bests = (from ii in itemIds
                         join i in itemRepo.Get(x => true, null, "") on ii equals i.Id
                         join c in catalogRepo.Get(x => true, null, "Vendor") on i.CatalogId equals c.Id
                         join ib in itemRepo.Get(x => true, null, "") on i.CategoryCode equals ib.CategoryCode
                         join cb in catalogRepo.Get(x => true, null, "Vendor") on ib.CatalogId equals cb.Id
                         select new BestItemOffer
                         {
                             ItemId = i.Id,
                             BestItemId = ib.Id,
                             OriginalPrice = i.Price,
                             OriginalDiscount = c.Discount,
                             OriginalPriceWithDiscount = i.Price * (1 - (decimal)0.01 * c.Discount),
                             BestDiscount = cb.Discount,
                             BestPrice = ib.Price,
                             BestPriceWithDiscount = ib.Price * (1 - (decimal)0.01 * cb.Discount),
                             CategoryCode = i.CategoryCode,
                             CatalogId = cb.Id,
                             OriginalCatalogCode = c.Code,
                             OriginalCatalogId = c.Id,
                             OriginalVendorId = c.VendorId,
                             OriginalVendorName = c.Vendor.Name,
                             BestCatalogCode = cb.Code,
                             BestCatalogId = cb.Id,
                             BestVendorId = cb.VendorId,
                             BestVendorName = cb.Vendor.Name,
                             ItemCode = i.Code,
                             BestItemCode = ib.Code
                         } into res
                         group res by new { res.CategoryCode, res.ItemId } into categories
                         select new {
                             key = categories.Key,
                             Best = categories.OrderBy(x=>x.BestPriceWithDiscount).FirstOrDefault()
                         })
                         .Select(x => x.Best).ToList();

            return bests;
        }
    }
}