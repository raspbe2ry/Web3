using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;

namespace Web3.DL
{
    public class VendorRepository
    {
        private GenericRepository<Vendor> repo = new GenericRepository<Vendor>(new ApplicationDbContext());

        public Select2PagedResult GetData(string searchTerm, int pageSize, int pageNumber)
        {
            Select2PagedResult dataResult = new Select2PagedResult();

            IEnumerable<Vendor> data;

            if (searchTerm != null)
                data = repo.Get(x => x.Code.Contains(searchTerm) || x.Name.Contains(searchTerm), null, "");
            else
                data = repo.Get(x => true, null, "");

            dataResult.Total = data.Count();

            data = data.Skip(pageSize*pageNumber).Take(pageSize);

            dataResult.Results = data.Select(x => new Select2OptionModel()
            {
                id = x.Id.ToString(),
                text = x.Name + " (" + x.Code + ")"
            }).ToList();

            return dataResult;
        }


    }
}