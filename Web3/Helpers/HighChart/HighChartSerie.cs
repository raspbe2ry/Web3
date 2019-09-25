using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.Helpers.HighChart
{
    public class HighChartSerie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<decimal> Data { get; set; }
    }
}