using System.Collections.Generic;
using System.Linq;
using Web3.DTO;
using Web3.Helpers;
using Web3.Models;
using System.Linq.Dynamic;

namespace Web3.DL
{
    public class SubOrderRepository
    {
        private GenericRepository<SubOrder> subOrderRepo;
        private GenericRepository<Models.Order> orderRepo;
        private GenericRepository<Item> itemRepo;
        private GenericRepository<Catalog> catalogRepo;
        private GenericRepository<Vendor> vendorRepo;

        public SubOrderRepository()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            subOrderRepo = new GenericRepository<SubOrder>(new ApplicationDbContext());
            orderRepo = new GenericRepository<Models.Order>(new ApplicationDbContext());
            itemRepo = new GenericRepository<Item>(new ApplicationDbContext());
            catalogRepo = new GenericRepository<Catalog>(new ApplicationDbContext());
            vendorRepo = new GenericRepository<Vendor>(new ApplicationDbContext());
        }

        public List<int> Create(List<DTOOrderItem> orderItems, int orderId)
        {
            var data = (from oi in orderItems
                        join i in itemRepo.Get(null, null, "") on oi.ItemId equals i.Id
                        join c in catalogRepo.Get(null, null, "") on i.CatalogId equals c.Id
                        join v in vendorRepo.Get(null, null, "") on c.VendorId equals v.Id
                        select new
                        {
                            ItemId = i.Id,
                            VendorId = v.Id,
                            Price = oi.Qty * i.Price * (1 - (decimal)0.01 * c.Discount)
                        } into vendorGroups
                        group vendorGroups by vendorGroups.VendorId into res
                        select new
                        {
                            VendorId = res.Key, 
                            SubOrderPrice = res.Sum(x=> x.Price)
                        }).ToList();

            List<int> subOrderIds = new List<int>();
            foreach(var so in data)
            {
                SubOrder subOrder = new SubOrder()
                {
                    OrderId = orderId,
                    VendorId = so.VendorId, 
                    Price = so.SubOrderPrice
                };

                subOrderIds.Add(subOrderRepo.Insert(subOrder));
            }

            Models.Order order = orderRepo.GetByID(orderId);
            order.Price = data.Sum(x => x.SubOrderPrice);
            orderRepo.Update(order);
            orderRepo.Save();

            return subOrderIds;                       
        }

        public DataResult<SubOrder> GetData(DataTableFilter dtFilter, int? orderId)
        {
            DataResult<SubOrder> dataResult = new DataResult<SubOrder>();

            IEnumerable<SubOrder> data;

            if (dtFilter.Search != null && dtFilter.Search.Value != null)
                data = subOrderRepo.Get(x => true
                , null, "Vendor");
            else
                data = subOrderRepo.Get(x => true, null, "Vendor");


            if (orderId != null)
                data = data.Where(x => x.OrderId == orderId);
            //if (filter != null)
            //{
            //    if (filter.BeginingDateFrom != null)
            //        data = data.Where(x => x.BeginingDate >= filter.BeginingDateFrom);
            //    if (filter.BeginingDateTo != null)
            //        data = data.Where(x => x.BeginingDate <= filter.BeginingDateTo);
            //    if (filter.EndDateFrom != null)
            //        data = data.Where(x => x.EndDate >= filter.EndDateFrom);
            //    if (filter.EndDateTo != null)
            //        data = data.Where(x => x.BeginingDate <= filter.EndDateTo);
            //    if (filter.VendorId != null)
            //        data = data.Where(x => x.VendorId == filter.VendorId.Value);
            //}

            dataResult.Count = data.Count();
            if (dtFilter.Order != null && dtFilter.Order != null && dtFilter.Order.Count != 0)
                data = data.OrderBy(dtFilter.Columns[dtFilter.Order[0].Column].Data + " " + dtFilter.Order[0].Dir);

            if(dtFilter.Columns != null)
                data = data.Skip(dtFilter.Start).Take(dtFilter.Length);

            dataResult.Data = data.ToList();

            return dataResult;
        }
    }
}