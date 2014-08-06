using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FxParser
{
    public class CurrencyStampEntity
    {
        public string CurrencyPair { get; set; }
        public string Time { get; set; }
        public double StartPrice { get; set; }
        public double MiddlePrice { get; set; }
        public double EndPrice { get; set; }

        public CurrencyStampEntity(CurrencyStamp parentObject)
        {
            CurrencyPair = parentObject.CurrencyPair;
            Time = String.Format("{0} {1}", parentObject.Time.ToShortDateString(), 
                                            parentObject.Time.ToShortTimeString());
            StartPrice = parentObject.StartPrice;
            MiddlePrice = parentObject.MiddlePrice;
            EndPrice = parentObject.EndPrice;
        }
    }
}