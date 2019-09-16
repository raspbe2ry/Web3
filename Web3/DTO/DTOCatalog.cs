using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;

namespace Web3.DTO
{
    public class DTOCatalog
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Code { get; set; }
        public string VendorName { get; set; }

        public static DataResult<DTOCatalog> MapToDTO(DataResult<Catalog> source)
        {
            DataResult<DTOCatalog> dataResult = new DataResult<DTOCatalog>();
            dataResult.Count = source.Count;

            dataResult.Data = source.Data.Select(x=> new DTOCatalog()
            {
                Id = x.Id, 
                Discount = x.Discount, 
                BeginingDate = x.BeginingDate, 
                EndDate = x.EndDate, 
                Code = x.Code,
                VendorName = x.Vendor.Name
            }).ToList();

            return dataResult;
        }

        public static DTOCatalog MapToDTO(Catalog source)
        {
            return new DTOCatalog()
            {
                BeginingDate = source.BeginingDate,
                EndDate = source.EndDate,
                Code = source.Code,
                Discount = source.Discount,
                Id = source.Id,
            };
        }
    }
}