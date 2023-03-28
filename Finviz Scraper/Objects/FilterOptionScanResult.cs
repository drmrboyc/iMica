using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    using Factories;

    public class FilterOptionScanResult
    {
        [BsonId]
        public ObjectId Id { get; }

        public DateTime ScanTime { get; }
        public FilterOption FilterOption { get; set; }
        public List<Stock> Stocks { get; set; }

        public FilterOptionScanResult(FilterOption option)
        {
            this.Id = ObjectId.NewObjectId();
            this.ScanTime = DateTime.Now;
            this.Stocks = new List<Stock>();
            this.FilterOption = option;
        }

        [BsonCtor]
        public FilterOptionScanResult(ObjectId _id, DateTime scanTime, BsonDocument filterOption, BsonArray stocks)
        {
            this.Id = _id;
            ScanTime = scanTime;
            
            //  Create the FilterOption from the Database BSON.
            FilterOption = new FilterOption(filterOption["_id"].AsString, filterOption["Name"].AsString, filterOption["DataURLFilter"].AsString, filterOption["ParentFilterId"].AsString);

            //  Create the list of stocks for this filter->option scan.
            Stocks = new List<Stock>();
            for (int x = 0; x < stocks.Count; x++)
                Stocks.Add(new Stock(stocks[x]["$id"]));
        }
    }
}