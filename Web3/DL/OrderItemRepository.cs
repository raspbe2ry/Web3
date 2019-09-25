using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.DTO;
using Web3.Models;

namespace Web3.DL
{
    public class OrderItemRepository
    {
        private GenericRepository<OrderItem> orderItemRepo;
        private GenericRepository<Item> itemRepo;
        private GenericRepository<Vendor> vendorRepo;
        private GenericRepository<Catalog> catalogRepo;
        private GenericRepository<Order> orderRepo;
        private GenericRepository<SubOrder> subOrderRepo;

        public OrderItemRepository()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            orderItemRepo = new GenericRepository<OrderItem>(dbContext);
            itemRepo = new GenericRepository<Item>(dbContext);
            vendorRepo = new GenericRepository<Vendor>(dbContext);
            catalogRepo = new GenericRepository<Catalog>(dbContext);
            orderRepo = new GenericRepository<Order>(dbContext);
            subOrderRepo = new GenericRepository<SubOrder>(dbContext);
        }

        public List<int> CreateOrderItems(List<DTOOrderItem> orderItems, int orderId)
        {
            var data = (from oi in orderItems
                        join i in itemRepo.Get(null, null, "") on oi.ItemId equals i.Id
                        join c in catalogRepo.Get(null, null, "") on i.CatalogId equals c.Id
                        join v in vendorRepo.Get(null, null, "") on c.VendorId equals v.Id
                        join so in subOrderRepo.Get(null, null, "") on v.Id equals so.VendorId
                        join o in orderRepo.Get(x=> x.Id == orderId, null, "") on so.OrderId equals o.Id
                        select new OrderItem()
                        {
                            ItemId = oi.ItemId,
                            Qty = oi.Qty,
                            CatalogDiscount = c.Discount,
                            ExpirationDate = null,
                            SubOrderId = so.Id
                        }).ToList();

            var orderItemIds = new List<int>();
            foreach (var oi in data)
            {
                int id = orderItemRepo.Insert(oi);
                orderItemIds.Add(id);
            }

            return orderItemIds;
        }

    }
}