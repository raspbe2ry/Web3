using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;

namespace Web3.DTO
{
    public class DTOItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public string CategoryCode { get; set; }
        public int? CatalogId { get; set; }

        public static DataResult<DTOItem> MapToDTO(DataResult<Item> source)
        {
            DataResult<DTOItem> dataResult = new DataResult<DTOItem>();
            dataResult.Count = source.Count;

            dataResult.Data = source.Data.Select(x => new DTOItem()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                CategoryCode = x.CategoryCode,
                CatalogId = x.CatalogId, 
                Code = x.Code
            }).ToList();

            return dataResult;
        }
    }
}