using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;

namespace Web3.DTO
{
    public class DTOSubOrder
    {
        public int Id { get; set; }
        public DateTime? ExpectedShipmentDate { get; set; }
        public decimal? Price { get; set; }
        public string VendorName { get; set; }
        public int? VendorId { get; set; }

        public static DataResult<DTOSubOrder> MapToDTO(DataResult<SubOrder> source)
        {
            DataResult<DTOSubOrder> dataResult = new DataResult<DTOSubOrder>();
            dataResult.Count = source.Count;

            dataResult.Data = source.Data.Select(x => new DTOSubOrder()
            {
                Id = x.Id,
                ExpectedShipmentDate = x.ExpectedShipmentDate,
                Price = x.Price,
                VendorId = x.VendorId,
                VendorName = x.Vendor.Name
            }).ToList();

            return dataResult;
        }
    }
}