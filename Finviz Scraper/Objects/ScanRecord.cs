using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    public class ScanRecord
    {
        [BsonId]
        public ObjectId Id { get; }

        public DateTime StartTime { get; }
        public DateTime EndTime { get; set; }

        public string ScanType { get; }
        public string ScanURL { get; }

        public ScanRecord(ScanFilterSettings scanSettings)
        {
            Id = ObjectId.NewObjectId();
            StartTime = DateTime.Now;
            ScanType = scanSettings.GetScanType().ToString();
            ScanURL = scanSettings.GetScanURLString();
        }

        [BsonCtor]
        public ScanRecord(ObjectId _id, DateTime startTime, DateTime endTime, string scanType, string scanURL)
        {
            this.Id = _id;
            StartTime = startTime;
            EndTime = endTime;
            ScanType = scanType;
            ScanURL = scanURL;
        }

        public void SetEndTime()
        {
            this.EndTime = DateTime.Now;
        }
    }
}
