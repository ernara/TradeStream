using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeStream
{
    public class Trade
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Title} price {Price}";
        }
    }
}
