using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    public class Stock
    {

        [BsonId]
        public string Symbol { get; }
        
        [BsonCtor]
        public Stock(string symbol)
        {
            this.Symbol = symbol;
        }

    }
}
