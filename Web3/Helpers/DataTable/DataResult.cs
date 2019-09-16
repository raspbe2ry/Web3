using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web3.Helpers
{
    public class DataResult<T>
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }
}