using System;
using System.Collections.Generic;
using System.Linq;
using Web3.Helpers;
using Web3.Models;
using System.Linq.Dynamic;
using Web3.Helpers.Filters;

namespace Web3.DL
{
    public class OrderRepository
    {
        private GenericRepository<SubOrder> subOrderRepo = new GenericRepository<SubOrder>(new ApplicationDbContext());
        private GenericRepository<Models.Order> orderRepo = new GenericRepository<Models.Order>(new ApplicationDbContext());

        public DataResult<Models.Order> GetData(DataTableFilter dtFilter, OrderFilter filter)
        {
            DataResult<Models.Order> dataResult = new DataResult<Models.Order>();

            IEnumerable<Models.Order> data;

            if (dtFilter.Search.Value != null)
                data = orderRepo.Get(x => x.Employee.FirstName.Contains(dtFilter.Search.Value) ||
                                    x.Employee.LastName.Contains(dtFilter.Search.Value)
                , null, "Employee");
            else
                data = orderRepo.Get(x => true, null, "Employee");

            if (filter != null)
            {
                if (filter.OrderDateFrom != null)
                    data = data.Where(x => x.Date >= filter.OrderDateFrom);
                if (filter.OrderDateTo != null)
                    data = data.Where(x => x.Date <= filter.OrderDateTo);
                if (filter.PriceFrom != null)
                    data = data.Where(x => x.Price >= filter.PriceFrom);
                if (filter.PriceTo != null)
                    data = data.Where(x => x.Price <= filter.PriceTo);
            }

            dataResult.Count = data.Count();
            if (dtFilter.Order != null && dtFilter.Order.Count != 0)
                data = data.OrderBy(dtFilter.Columns[dtFilter.Order[0].Column].Data + " " + dtFilter.Order[0].Dir);

            data = data.Skip(dtFilter.Start).Take(dtFilter.Length);

            dataResult.Data = data.ToList();

            return dataResult;
        }

        public int CreateOrder()
        {
            Models.Order order = new Models.Order()
            {
                Date = DateTime.Now,
                UserId = 1
            };

            return orderRepo.Insert(order);
        }
    }
}