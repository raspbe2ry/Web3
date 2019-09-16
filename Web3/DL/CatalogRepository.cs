using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;
using System.Linq.Dynamic;
using Web3.Helpers.Filters;

namespace Web3.DL
{
    public class CatalogRepository
    {
        private static GenericRepository<Catalog> repo = new GenericRepository<Catalog>(new ApplicationDbContext());

        public static DataResult<Catalog> GetData(DataTableFilter dtFilter, CatalogFilter filter)
        {
            DataResult<Catalog> dataResult = new DataResult<Catalog>();

            IEnumerable<Catalog> data;

            if(dtFilter.Search.Value != null)
                data = repo.Get(x => x.Code.Contains(dtFilter.Search.Value), null, "Vendor");
            else
                data = repo.Get(x=> true, null, "Vendor");
            
            if(filter != null)
            {
                if (filter.BeginingDateFrom != null)
                    data = data.Where(x => x.BeginingDate >= filter.BeginingDateFrom);
                if (filter.BeginingDateTo != null)
                    data = data.Where(x => x.BeginingDate <= filter.BeginingDateTo);
                if (filter.EndDateFrom != null)
                    data = data.Where(x => x.EndDate >= filter.EndDateFrom);
                if (filter.EndDateTo != null)
                    data = data.Where(x => x.BeginingDate <= filter.EndDateTo);
                if (filter.VendorId != null)
                    data = data.Where(x => x.VendorId == filter.VendorId.Value);
            }

            dataResult.Count = data.Count();
            if(dtFilter.Order != null && dtFilter.Order.Count != 0)
                data = data.OrderBy(dtFilter.Columns[dtFilter.Order[0].Column].Data+" "+dtFilter.Order[0].Dir);

            data = data.Skip(dtFilter.Start).Take(dtFilter.Length);

            dataResult.Data = data.ToList();

            return dataResult;
        }

        public static Catalog GetIndividual(int id)
        {
            Catalog individual = repo.GetByID(id);

            return individual;
        }
    }
}