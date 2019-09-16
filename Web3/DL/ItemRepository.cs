using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Helpers.Filters;
using Web3.Models;
using System.Linq.Dynamic;

namespace Web3.DL
{
    public class ItemRepository
    {
        private static GenericRepository<Item> repo = new GenericRepository<Item>(new ApplicationDbContext());

        public static DataResult<Item> GetData(DataTableFilter dtFilter, ItemFilter filter)
        {
            DataResult<Item> dataResult = new DataResult<Item>();

            IEnumerable<Item> data;

            if (dtFilter.Search.Value != null)
                data = repo.Get(x => x.Code.Contains(dtFilter.Search.Value) ||
                                     x.Name.Contains(dtFilter.Search.Value) 
                                     , null, "");
            else
                data = repo.Get(x => true, null, "");

            if (filter != null)
            {
                if(filter.CatalogId != null)
                    data = data.Where(x => x.CatalogId == filter.CatalogId);
            }

            dataResult.Count = data.Count();
            if (dtFilter.Order != null && dtFilter.Order.Count != 0)
                data = data.OrderBy(dtFilter.Columns[dtFilter.Order[0].Column].Data + " " + dtFilter.Order[0].Dir);

            data = data.Skip(dtFilter.Start).Take(dtFilter.Length);

            dataResult.Data = data.ToList();

            return dataResult;
        }
    }
}