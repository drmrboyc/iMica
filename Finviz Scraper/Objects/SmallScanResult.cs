using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    public class SmallScanResult
    {
        public ObjectId Id { get; }
        public DateTime ScanTime { get; }

        public Stock Stock { get; set; }

        public decimal Price { get; }
        public decimal Change { get; }
        public long Volume { get; }
        public decimal RelativeVolume { get; }

        public SmallScanResult(Stock stock, decimal price, decimal change, long volume, decimal relativeVolume)
        {
            this.Id = ObjectId.NewObjectId();
            ScanTime = DateTime.Now;
            Stock = stock;
            Price = price;
            Change = change;
            Volume = volume;
            RelativeVolume = relativeVolume;
        }

        [BsonCtor]
        public SmallScanResult(ObjectId _id, DateTime scanTime, BsonDocument stock, decimal price, decimal change, long volume, decimal relativeVolume)
        {
            this.Id = _id;
            ScanTime = scanTime;
            Stock = new Stock(stock["_id"].AsString);
            Price = price;
            Change = change;
            Volume = volume;
            RelativeVolume = relativeVolume;
        }
    }
}