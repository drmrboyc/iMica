using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finviz_Scraper.Objects
{
    public class ScanFilterSettings
    {
        public enum ScanType
        {
            FILTER_OPTION,
            ALL_STOCKS_BASE_INFO,
            ALL_STOCKS_ALL_INFO
        }

        [Flags]
        public enum ScanFilters
        {
            PriceUnder20 = 1,
            AvgVolumeOver400k = 2,
            FloatUnder50M = 4,
            ExcludeFunds = 8,

            None = 0,
            All = PriceUnder20 | AvgVolumeOver400k | FloatUnder50M | ExcludeFunds
        }

        private bool FilterPriceUnder20 = true;
        private readonly string FilterStringPriceUnder20 = "sh_price_u20";

        private bool FilterAvgVolumeOver400k = true;
        private readonly string FilterStringAvgVolOver400k = "sh_avgvol_o400";

        private bool FilterFloatUnder50M = false;
        private readonly string FilterStringFloatUnder50M = "sh_float_u50";

        private bool FilterExcludeFunds = true;
        private readonly string FilterStringExcludeFunds = "ind_stocksonly";

        private List<string> AdditionalFilters = new List<string>();

        private readonly string SortByChangeString = "&o=-change";

        private readonly string LargePageResultsString = "?v=521";
        private readonly List<string> SmallPageResultsStrings = new List<string>() { "?v=111", "?v=121", "?v=131", "?v=141", "?v=161", "?v=171" };

        private readonly string FinvizBaseURI = "https://finviz.com/screener.ashx";
        private readonly static string ScanURLStringAllFilters = "https://finviz.com/screener.ashx?ft=4";

        private string ScanURLString = "";

        //private string SmallScanURL = "https://finviz.com/screener.ashx?v=521&f=ind_stocksonly,sh_avgvol_o400,sh_price_u20&o=-change";
        //private string TestingURL = "https://finviz.com/screener.ashx?v=521&f=ind_stocksonly,sh_avgvol_o400,sh_price_u20,ta_perf2_d10o&ft=4&o=-change";

        private readonly ScanType scanType;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////// CONSTRUCTORS

        private ScanFilterSettings() {}

        public ScanFilterSettings(ScanType scanType)
        {
            this.scanType = scanType;
            BuildURLString();
        }

        public ScanFilterSettings(ScanType scanType, List<string> additionalFilters)
        {
            this.scanType = scanType;
            this.AdditionalFilters.Concat(additionalFilters);
            BuildURLString();
        }

        public ScanFilterSettings(ScanType scanType, ScanFilters scanFilters)
        {
            this.scanType = scanType;
            SetActiveFiltersTo(scanFilters);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////// ACCESSORS            

        public string GetScanURLString()
        {
            BuildURLString();
            return ScanURLString;
        }

        public static string GetFiltersAndOptionsOnlyString()
        {
            return ScanURLStringAllFilters;
        }

        public ScanType GetScanType()
        {
            return scanType;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////// MODIFIERS            

        public void AddAdditionalFilter(string filterString)
        {
            this.AdditionalFilters.Add(filterString);
            BuildURLString();
        }

        public void RemoveAllAdditionalFilters()
        {
            this.AdditionalFilters = new List<string>();
        }

        public void SetActiveFiltersTo(ScanFilters scanFilters)
        {
            this.FilterPriceUnder20 = scanFilters.HasFlag(ScanFilters.PriceUnder20);
            this.FilterAvgVolumeOver400k = scanFilters.HasFlag(ScanFilters.AvgVolumeOver400k);
            this.FilterFloatUnder50M = scanFilters.HasFlag(ScanFilters.FloatUnder50M);
            this.FilterExcludeFunds = scanFilters.HasFlag(ScanFilters.ExcludeFunds);

            BuildURLString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////// URL STRING BUILDER

        private void BuildURLString()
        {
            string tmpURL = FinvizBaseURI;

            tmpURL += scanType == ScanType.ALL_STOCKS_ALL_INFO ? SmallPageResultsStrings[0] : LargePageResultsString;

            tmpURL += "&f=";

            tmpURL += FilterExcludeFunds ? FilterStringExcludeFunds + "," : "";
            tmpURL += FilterAvgVolumeOver400k ? FilterStringAvgVolOver400k + "," : "";
            tmpURL += FilterFloatUnder50M ? FilterStringFloatUnder50M + "," : "";
            tmpURL += FilterPriceUnder20 ? FilterStringPriceUnder20 + "," : "";

            foreach (string filter in AdditionalFilters)
            {
                tmpURL += filter + ",";
            }

            tmpURL += SortByChangeString;

            ScanURLString = tmpURL;
        }
    }
}
