using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB.Engine;
using LiteDB;

namespace Finviz_Scraper.Objects
{
    using Factories;

    public class Filter
    {
        [BsonId]
        public string URL { get; }

        public string Name { get; }       
        public List<FilterOption> FilterOptions { get; }
        
        public Filter(string url, string name)
        {
            this.URL = url;
            Name = name;
            FilterOptions = new List<FilterOption>();
        }
        
        [BsonCtor]
        public Filter (string url, string name, BsonArray filterOptions)
        {
            this.URL = url;
            Name = name;

            FilterOptions = new List<FilterOption>();
            for (int x = 0; x < filterOptions.Count; x++)
                FilterOptions.Add(new FilterOption(filterOptions[x]["_id"], filterOptions[x]["Name"], filterOptions[x]["DataURLFilter"], filterOptions[x]["ParentFilterId"]));
        }

        public void AddOptionToFilter(FilterOption filterOption)
        {
            FilterOptions.Add(filterOption);
        }
    }
}
