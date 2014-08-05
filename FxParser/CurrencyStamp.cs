using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FxParser
{
    public class CurrencyStamp
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public DateTime Time { get; set; }
        public double StartPrice { get; set; }
        public double MiddlePrice { get; set; }
        public double EndPrice { get; set; }
    }
}