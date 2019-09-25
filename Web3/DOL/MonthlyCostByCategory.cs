using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.DL;
using Web3.Helpers.HighChart;
using Web3.Models;

namespace Web3.DOL
{
    public class MonthlyCostByCategory
    {
        private GenericRepository<Order> orderRepo = new GenericRepository<Order>(new ApplicationDbContext());
        private GenericRepository<SubOrder> subOrderRepo = new GenericRepository<SubOrder>(new ApplicationDbContext());
        private GenericRepository<OrderItem> orderItemRepo = new GenericRepository<OrderItem>(new ApplicationDbContext());
        private GenericRepository<Item> itemRepo = new GenericRepository<Item>(new ApplicationDbContext());
        private GenericRepository<Catalog> catalogRepo = new GenericRepository<Catalog>(new ApplicationDbContext());

        public HighChartData GetForYear(int year)
        {
            List<int> months = new List<int>();
            for (int i = 1; i <= 12; i++)
                months.Add(i);

            var categoriesPerMonth = (from i in itemRepo.Get(null, null, "")
                                      group i by i.CategoryCode into cc
                                      join m in months on 1 equals 1
                                       select new {
                                           Category = cc.Key,
                                           Month = m
                                       }).ToList();

            var data = (from o in orderRepo.Get(x => x.Date.Year == year, null, "")
                        join so in subOrderRepo.Get(null, null, "") on o.Id equals so.OrderId
                        join oi in orderItemRepo.Get(null, null, "") on so.Id equals oi.Id
                        join i in itemRepo.Get(null, null, "Catalog") on oi.ItemId equals i.Id
                        select new
                        {
                            Category = i.CategoryCode,
                            Price = i.Price * (1 - (decimal)0.01 * oi.CatalogDiscount) * oi.Qty,
                            Month = o.Date.Month
                        } into p
                        group p by new { p.Category, p.Month } into montlyPerCategory
                        select new
                        {
                            Category = montlyPerCategory.Key.Category,
                            Month = montlyPerCategory.Key.Month,
                            Price = montlyPerCategory.Sum(x => x.Price)
                        }).ToList();

            var final = (from cm in categoriesPerMonth
                         from d in data.Where(x=>x.Month == cm.Month && x.Category.Equals(cm.Category)).DefaultIfEmpty()
                        select new
                        {
                            Category = cm.Category,
                            Month = cm.Month,
                            Price = d != null ? d.Price : 0
                        } into hh
                        group hh by hh.Category into perCategory
                        select new HighChartSerie()
                        {
                            Id = perCategory.Key,
                            Name = perCategory.Key,
                            Data = perCategory.Select(x => x.Price).ToList()
                        }).ToList();

            HighChartData result = new HighChartData()
            {
                Series = final
            };

            return result;
        }
    }
}