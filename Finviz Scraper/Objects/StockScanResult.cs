using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    using Factories;

    public class StockScanResult
    {
        [BsonId]
        public ObjectId Id { get; }

        public DateTime ScanTime { get; }

        //[BsonRef("stock_symbols")]
        public Stock Stock { get; set; }

        public string Company { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Country { get; set; }
        public long MarketCap { get; set; }
        public decimal PE { get; set; }
        public decimal Price { get; set; }
        public decimal Change { get; set; }
        public long Volume { get; set; }
        public decimal RelativeVolume { get; set; }

        public StockScanResult()
        {
            this.Id = ObjectId.NewObjectId();
            ScanTime = DateTime.Now;
        }
        
        [BsonCtor]
        public StockScanResult(ObjectId _id, DateTime scanTime, BsonDocument stock, string company, string sector, string industry, string country, 
            long marketCap, decimal pe, decimal price, decimal change, long volume, decimal relativeVolume)
        {
            this.Id = _id;
            ScanTime = scanTime;
            Stock = new Stock(stock["_id"].AsString);
            Company = company;
            Sector = sector;
            Industry = industry;
            Country = country;
            MarketCap = marketCap;
            PE = pe;
            Price = price;
            Change = change;
            Volume = volume;
            RelativeVolume = relativeVolume;
        }
    }
}