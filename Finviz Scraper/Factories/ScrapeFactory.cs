using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Parsing;
using ScrapySharp.Core;
using HtmlAgilityPack;
using System.Globalization;

namespace Finviz_Scraper.Factories
{
    using Objects;
    using Utilities;

    public static class ScrapeFactory
    {
        public static event EventHandler<ScannerPageUpdateEventArgs> ScannerPageUpdate = delegate { };

        static void ScrapySharp()
        {

        }
        
        private static void CallUIUpdateEvent(int maxPages, int curPage)
        {
            if (FormMain.StartedByCommandLine == true)
                return;

            ScannerPageUpdate(null, new ScannerPageUpdateEventArgs(null, maxPages, curPage));
        }

        public static dynamic PerformScanAsync(ScanFilterSettings settings)
        {
            //  Create and instantiate the return list.
            List<Stock> filteredStockList = new List<Stock>();
            List<SmallScanResult> smallScanList = new List<SmallScanResult>();
            List<StockScanResult> stockScanList = new List<StockScanResult>();

            //  Load the initial scan webpage.
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(settings.GetScanURLString());
            
            //  Get a list of the URL page numbers in the results.
            List<int> pageNums = GetURLPagesList(doc, out int maxPages);
   
            for (int pg = 0; pg < maxPages; pg++)
            {
                //  Update the UI with the scan's current progress.
                CallUIUpdateEvent(maxPages, (pg + 1));

                //  If we're on a new page, load that page to parse the results.
                if (pg > 0)
                    doc = web.Load(settings.GetScanURLString() + "&r=" + pageNums[pg - 1].ToString());

                //  Based on our scan type scrape the page for the information we need.
                switch (settings.GetScanType())
                {
                    case ScanFilterSettings.ScanType.FILTER_OPTION:
                        filteredStockList.AddRange(ScrapeLargePageForSymbolsOnly(doc));
                        break;
                    case ScanFilterSettings.ScanType.ALL_STOCKS_BASE_INFO:
                        smallScanList.AddRange(ScrapeLargePageForBaseInfo(doc));
                        break;
                    case ScanFilterSettings.ScanType.ALL_STOCKS_ALL_INFO:
                        // NOT IMPLEMENTED
                        break;
                    default:
                        break;
                }

                //  Delay loading the next page of results by 2-3 seconds.
                DelayCurrentThread(2000, 3000);
            }

            //  Update the UI to show that the scan has completed.
            CallUIUpdateEvent(0, 0);

            if (settings.GetScanType() == ScanFilterSettings.ScanType.FILTER_OPTION)
                return filteredStockList;
            else if (settings.GetScanType() == ScanFilterSettings.ScanType.ALL_STOCKS_BASE_INFO)
                return smallScanList;
            else
                return stockScanList;
        }

        private static List<Stock> ScrapeLargePageForSymbolsOnly(HtmlDocument doc)
        {
            List<Stock> retList = new List<Stock>();

            //  Get all rows of stocks
            var stockRows = doc.DocumentNode.CssSelect("table.t-home-table > tr");
            //  Iterate through each row 
            foreach (HtmlNode stock in stockRows)
            {
                //  Get stock's innertext after removing the '&nbsp;' (html spaces)
                string[] cells = stock.InnerText.Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);

                //  Add the stock to our return scan results list
                retList.Add(new Stock(cells[0].Trim()));                
            }

            return retList;
        }

        private static List<SmallScanResult> ScrapeLargePageForBaseInfo(HtmlDocument doc)
        {
            List<SmallScanResult> retList = new List<SmallScanResult>();

            //  Get all rows of stocks
            var stockRows = doc.DocumentNode.CssSelect("table.t-home-table > tr");
            //  Iterate through each row 
            foreach (HtmlNode stock in stockRows)
            {
                //  Get stock's innertext after removing the '&nbsp;' (html spaces)
                string[] cells = stock.InnerText.Split(new string[] { "&nbsp;" }, StringSplitOptions.RemoveEmptyEntries);

                //  Parse the stock's data into variables
                decimal.TryParse(cells[1].Trim(), out decimal price);
                decimal.TryParse(cells[2].TrimEnd('%'), out decimal change);
                Int64.TryParse(cells[3].Trim(), out long volume);
                decimal.TryParse(cells[4].Trim(), out decimal relVolume);

                //  Add the stock to our return scan results list
                retList.Add(new SmallScanResult(new Stock(cells[0].Trim()), price, change, volume, relVolume));
            }

            return retList;
        }
        
        //public static void ScrapeStocksFromBaseScan(out List<Stock> stocks, out List<StockScanResult> stockScanResults )
        //{
        //    //  The local lists to populate and finally copy into the out parameter lists.
        //    List<Stock> retStocks = new List<Stock>();
        //    List<StockScanResult> retStockScanResults = new List<StockScanResult>();

        //    //  Scans all stocks for data using the base criteria.
        //    HtmlWeb web = new HtmlWeb();
        //    HtmlDocument doc = web.Load(FinvizBaseScan);

        //    var nodes = doc.DocumentNode.CssSelect("select.pages-combo");
        //    List<int> resultPageNums = GetAllResultPageLinks(nodes.First().ChildNodes.ToList());
            
        //    //  Get the max number of pages.
        //    int maxPages = (int)Math.Ceiling((double)resultPageNums.Last() / 20);

        //    //  Iterate through all the result pages.
        //    foreach (int pageNum in resultPageNums)
        //    {
        //        //  Get the current page and raise an event to update our UI with the max pages and current page being scanned.
        //        int curPage = (int)Math.Ceiling((double)pageNum / 20);
        //        ScannerPageUpdate(null, new ScannerPageUpdateEventArgs(null, maxPages, curPage));

        //        // ============================================================================================================================================
        //        var darkStocks = doc.DocumentNode.CssSelect("tr.table-dark-row-cp");
        //        var lightStocks = doc.DocumentNode.CssSelect("tr.table-light-row-cp");
        //        var stockHtmlNodes = darkStocks.Concat(lightStocks);

        //        foreach (HtmlNode stockNode in stockHtmlNodes)
        //        {
        //            Stock tmpStock = new Stock();
        //            StockScanResult tmpStockScanResult = new StockScanResult();
        //            GetStocksInfoFromHtml(stockNode, ref tmpStock, ref tmpStockScanResult);
        //            retStocks.Add(tmpStock);
        //            retStockScanResults.Add(tmpStockScanResult);
        //        }
        //        // ============================================================================================================================================

        //        //  Delay loading the next results page by 2-3 seconds.
        //        Random random = new Random();
        //        int randomNumber = random.Next(2000, 3000);
        //        Thread.Sleep(randomNumber);

        //        //  Load the next page of results
        //        doc = web.Load(FinvizBaseScan + "&r=" + pageNum.ToString());
        //    }

        //    //  Update the UI to show that the scan has completed.
        //    ScannerPageUpdate(null, new ScannerPageUpdateEventArgs(null, 0, 0));

        //    //  Assign the parameter lists (by out) to the populated local lists.
        //    stocks = retStocks;
        //    stockScanResults = retStockScanResults;
        //}

        private static void GetStocksInfoFromHtml(HtmlNode _node, ref Stock _stock, ref StockScanResult _stockScanResult)
        { 
            int cellIterator = 0;

            foreach (HtmlNode cell in _node.ChildNodes)
            {
                if (cell.Name == "#text")
                    continue;

                bool success = false;
                switch (cellIterator)
                {
                    case 0:
                        break;
                    case 1:
                        string stockSymbol = cell.InnerText.Trim();
                        _stockScanResult.Stock = _stock;
                        break;
                    case 2:
                        _stockScanResult.Company = cell.InnerText.Trim();
                        break;
                    case 3:
                        _stockScanResult.Sector = cell.InnerText.Trim();
                        break;
                    case 4:
                        _stockScanResult.Industry = cell.InnerText.Trim();
                        break;
                    case 5:
                        _stockScanResult.Country = cell.InnerText.Trim();
                        break;
                    case 6:
                        _stockScanResult.MarketCap = ConvertMarketCapToLong(cell.InnerText.Trim());
                        break;
                    case 7:
                        success = decimal.TryParse(cell.InnerText.Trim(), out decimal pe);
                        _stockScanResult.PE = (success == true) ? pe : 0;
                        break;
                    case 8:
                        success = decimal.TryParse(cell.InnerText.Trim(), out decimal price);
                        _stockScanResult.Price = (success == true) ? price : 0;
                        break;
                    case 9:
                        success = decimal.TryParse(cell.InnerText.Substring(0, cell.InnerText.Length - 1).Trim(), out decimal change);
                        _stockScanResult.Change = (success == true) ? change : 0;
                        break;
                    case 10:
                        success = Int64.TryParse(cell.InnerText.Trim(), System.Globalization.NumberStyles.Number, CultureInfo.InvariantCulture, out long volume);
                        _stockScanResult.Volume = (success == true) ? volume : 0;
                        break;
                    default:
                        break;
                }
                cellIterator++;
            }
        }

        #region SCAN_FILTERS & OPTIONS

        public static void GetAllFiltersAndOptions(out List<Filter> filters)
        {
            //  The local Filter list to copy into the out parameter filter list.
            List<Filter> retFilters = new List<Filter>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(ScanFilterSettings.GetFiltersAndOptionsOnlyString());
            var nodes = doc.DocumentNode.CssSelect("select.screener-combo-text");
            foreach (HtmlNode node in nodes)
            {
                var filterName = node.ParentNode.PreviousSibling.PreviousSibling.InnerText.Trim();
                var dataFilter = node.GetAttributeValue("data-filter", "").Trim();

                Filter tmpFilter = new Filter(dataFilter, filterName);

                foreach (HtmlNode child in node.ChildNodes)
                {
                    if (child.Name == "option")
                    {
                        string childText = child.InnerText.Trim();

                        if (childText == "Any" || childText == "Custom (Elite only)" || tmpFilter.FilterOptions.Exists(x => x.Name == childText))
                            continue;

                        var childValue = child.GetAttributeValue("value", "").Trim();

                        tmpFilter.AddOptionToFilter(new FilterOption(childText, childValue, tmpFilter));
                    }
                }

                retFilters.Add(tmpFilter);
            }

            filters = retFilters;
        }

        #endregion

        #region UTILITY FUNCTIONS

        private static List<int> GetURLPagesList(HtmlDocument doc, out int maxPages)
        {
            //  Get the number of pages we will be iterating through.
            var nodes = doc.DocumentNode.CssSelect("select.pages-combo");

            //  If the scan returned NO results, then return an empty list of pages.
            if (nodes.Count() == 0)
            {
                maxPages = 0;
                return null;
            }

            //  Get a list of the stock number count that each page begins with. (ie. 1, 501, 1001, etc.)
            List<int> resultPageNums = GetAllResultPageLinks(nodes.First().ChildNodes.ToList());

            //  If we have multiiple pages of results then 
            maxPages = resultPageNums.Count <= 0 ? 1 : (int)Math.Ceiling((double)resultPageNums.Last() / 500);
            return resultPageNums;
        }

        private static List<int> GetAllResultPageLinks(List<HtmlNode> resultPagesList)
        {
            List<int> pageNums = new List<int>();

            foreach (HtmlNode pageOption in resultPagesList)
            {
                if (pageOption.Name == "option")
                {
                    var childValue = pageOption.GetAttributeValue("value", "").Trim();
                    int.TryParse(childValue, out int pageNum);

                    if (pageNum > 1)
                        pageNums.Add(pageNum);
                }
            }

            return pageNums;
        }

        private static long ConvertMarketCapToLong(string _numString)
        {
            string multiplier = _numString.Substring(_numString.Length - 1, 1);

            if (multiplier != "M" && multiplier != "B")
                return 0;

            long numMultiplier = multiplier == "M" ? 1000000 : 1000000000;

            decimal.TryParse(_numString.Substring(0, _numString.Length - 1), out decimal quantityAbbr);

            return Convert.ToInt64(numMultiplier * quantityAbbr);
        }

        private static void DelayCurrentThread(int minMilliseconds, int maxMilliseconds)
        {
            Random random = new Random();
            int randomNumber = random.Next(minMilliseconds, maxMilliseconds);
            Thread.Sleep(randomNumber);
        }

        #endregion
    }

    #region SCAN PROGRESS UPDATE

    public class ScannerPageUpdateEventArgs : EventArgs
    {
        public Exception Error { get; }
        public int TotalPages { get; }
        public int FinishedPage { get; }

        public ScannerPageUpdateEventArgs(Exception _ex, int _totalPages, int _finishedPage)
        {
            Error = _ex;
            TotalPages = _totalPages;
            FinishedPage = _finishedPage;
        }        
    }

    #endregion

    public static class Extension
    {
        public static List<T> JoinLists<T>(this List<T> first, List<T> second)
        {
            if (first == null)
            {
                return second;
            }
            if (second == null)
            {
                return first;
            }

            return first.Concat(second).ToList();
        }
    }
}


//  FINVIZ SOURCE INFO:
//  
//      - URL to SHOW ALL FILTERS: https://finviz.com/screener.ashx?ft=4
//
//      - URL for AVG VOL 400k & PRICE < 20 "https://finviz.com/screener.ashx?v=111&f=sh_avgvol_o400,sh_price_u20"
//

//  EXAMPLES:
//
//      return new Uri(new Uri(baseUrl), document.DocumentNode.CssSelect(".table-of-content .head-row td.download a.text-pdf").Single().Attributes["href"].Value).ToString();
//
//    SELECTING A NODE:
//      HtmlWeb web = new HtmlWeb();
//      HtmlDocument doc = web.Load("http://www.stackoverflow.com");
//      var page = doc.DocumentNode.SelectSingleNode("//body");
//
//    EXTRACTING A TABLE:
//      var hw = new HtmlWeb();
//      doc = hw.Load("http://www.nasdaq.com/earnings/earnings-calendar.aspx");
//      foreach (HtmlNode row in doc.DocumentNode.Descendants("table").FirstOrDefault(_ => _.Id.Equals("ECCompaniesTable")).Descendants("tr"))
//      {
//          Console.WriteLine(row.InnerText);
//      }

