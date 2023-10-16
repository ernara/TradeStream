using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeStream
{
    public class TradeGenerator
    {
        private List<string> titles;

        public TradeGenerator()
        {
            titles = new List<string>() { "Vilk", "ABC", "VYCKA", "Good" };
        }

        public Trade NextTrade()
        {
            return new Trade()
            {
                Title = titles[Random.Shared.Next(titles.Count)],
                Price = (decimal)Random.Shared.NextDouble() * 100,
            };
        }
    }
}
