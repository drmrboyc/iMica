using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using LiteDB.Engine;

namespace Finviz_Scraper.Factories
{
    using Objects;

    public static class DBFactory
    {
        private static readonly string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FinvizScraper");
        //private static readonly string DBPath = Path.Combine(@"D:\iMICA", "FinvizScraper");

        private static LiteDatabase MyDatabase
        {
            get
            {
                return new LiteDatabase(new ConnectionString(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString
                            .Replace("%AppData%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))));
            }
        }

        public static List<Filter> Filters = new List<Filter>();
        public static int NumFilterOptions = 0;

        public static readonly string TableName_Filters = "filters";
        public static readonly string TableName_StockSymbols = "stock_symbols";

        public static readonly string TableName_ScanRecords = "scan_records";
        public static readonly string TableName_SmallScanResults = "small_scan_results";
        public static readonly string TableName_FilterOptionsScanResults = "filter_option_scan_results";
        public static readonly string TableName_StockScanResults = "stock_scan_results";               

        static DBFactory() { }

        #region -- READ FROM DATABASE --

        public static List<Filter> RetrieveFilterList()
        {
            using (var db = MyDatabase)
            {
                var col = db.GetCollection<Filter>(TableName_Filters);
                Filters = col.Query().ToList();
            }

            //  Update the number of options available.
            NumFilterOptions = 0;
            foreach (Filter fltr in Filters)
                NumFilterOptions += fltr.FilterOptions.Count;

            return Filters;
        }

        public static void RetrieveSmallScanResults(out List<SmallScanResult> _results)
        {
            using (var db = MyDatabase)
            {
                var col = db.GetCollection<SmallScanResult>(TableName_SmallScanResults);
                _results = col.Query().ToList();
            }
        }

        public static List<FilterOptionScanResult> RetrieveFilterOptionScanResults()
        {
            List<FilterOptionScanResult> ret = new List<FilterOptionScanResult>();
            using (var db = MyDatabase)
            {
                var col = db.GetCollection<FilterOptionScanResult>(TableName_FilterOptionsScanResults);
                ret = col.Query().ToList();                                
            }
            return ret;
        }

        public static List<ScanRecord> RetrieveScanRecords()
        {
            using (var db = MyDatabase)
            {
                var col = db.GetCollection<ScanRecord>(TableName_ScanRecords);
                return col.Query().ToList();
            }
        }

        #endregion

        #region -- WRITE TO DATABASE --

        public static void UpdateFiltersInDatabase(List<Filter> filters)
        {
            using (var db = MyDatabase)
            {
                var dbFilters = db.GetCollection<Filter>(TableName_Filters);
                dbFilters.Upsert(filters);
                dbFilters.EnsureIndex(x => x.URL);
            }
        }

        public static void WriteScanRecord(ScanRecord scanRecord)
        {
            using (var db = MyDatabase)
            {
                var dbScanRecords = db.GetCollection<ScanRecord>(TableName_ScanRecords);
                dbScanRecords.Insert(scanRecord);
                dbScanRecords.EnsureIndex(x => x.ScanType);                
            }
        }

        public static void WriteSmallScanResult(List<SmallScanResult> smallScanResults)
        {
            using (var db = MyDatabase)
            {
                //  Insert all scan results into the db.
                var dbStockScanResults = db.GetCollection<SmallScanResult>(TableName_SmallScanResults);
                dbStockScanResults.InsertBulk(smallScanResults, smallScanResults.Count);
                dbStockScanResults.EnsureIndex(x => x.Stock);

                //  Update the Stocks collection to reflect any new stocks we have found.
                var dbStocks = db.GetCollection<Stock>(TableName_StockSymbols);
                dbStocks.Upsert(smallScanResults.Select(o => o.Stock).ToList());
            }
        }

        public static void WriteStockScanResult(List<StockScanResult> stockScanResults)
        {
            using (var db = MyDatabase)
            {
                //  Insert all scan results into the db.
                var dbStockScanResults = db.GetCollection<StockScanResult>(TableName_StockScanResults);
                dbStockScanResults.InsertBulk(stockScanResults, stockScanResults.Count);
                dbStockScanResults.EnsureIndex(x => x.Stock);
            }
        }

        public static void WriteFilterOptionScanResult(List<FilterOptionScanResult> scanResults)
        {
            using (var db = MyDatabase)
            {
                //  Insert all scan results into the db.
                var dbFilterOptionScanResults = db.GetCollection<FilterOptionScanResult>(TableName_FilterOptionsScanResults);
                dbFilterOptionScanResults.InsertBulk(scanResults, scanResults.Count);
                dbFilterOptionScanResults.EnsureIndex(x => x.FilterOption.URL);
                dbFilterOptionScanResults.EnsureIndex(x => x.Stocks);
            }
        }

        #endregion

        //private static void InsertUpdateStockList(List<Stock> stocksList, ref LiteDatabase db)
        //{
        //    var dbStocks = db.GetCollection<Stock>(TableName_StockSymbols);
        //    dbStocks.Upsert(stocksList);
        //    dbStocks.EnsureIndex(x => x.Symbol);
        //}

        public static void CheckFileSystemIntegrity()
        {
            try
            {
                if (!Directory.Exists(DBPath))
                {
                    if (File.Exists(DBPath))
                        File.Delete(DBPath);

                    Directory.CreateDirectory(DBPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to verify or create the database file system. Please re-run iMICA as Administrator.");
                Console.WriteLine("ERROR IN FILE SYSTEM CHECK: " + ex.Message);
            }
        }
    }
}