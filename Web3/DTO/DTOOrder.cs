using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web3.Helpers;
using Web3.Models;

namespace Web3.DTO
{
    public class DTOOrder
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Price { get; set; }
        public string EmployeeName { get; set; }

        public static DataResult<DTOOrder> MapToDTO(DataResult<Models.Order> source)
        {
            DataResult<DTOOrder> dataResult = new DataResult<DTOOrder>();
            dataResult.Count = source.Count;

            dataResult.Data = source.Data.Select(x => new DTOOrder()
            {
                Id = x.Id,
                OrderDate = x.Date,
                Price = x.Price,
                EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName
            }).ToList();

            return dataResult;
        }

    }
}