using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;


namespace Finviz_Scraper.Objects
{
    public class FilterOption
    {
        [BsonId]
        public string URL { get; }

        public string Name { get; }
        public string DataURLFilter { get; }
        public string ParentFilterId { get; }

        public FilterOption(string name, string dataURLFilter, Filter parentFilter)
        {
            //  Create a primary key combination of the option data url filter + the parent filter's name
            this.URL = parentFilter.URL + "_" + dataURLFilter;

            Name = name;
            DataURLFilter = dataURLFilter;
            ParentFilterId = parentFilter.URL;            
        }

        [BsonCtor]
        public FilterOption(string url, string name, string dataURLFilter, string parentFilterId)
        {
            this.URL = url;
            Name = name;
            DataURLFilter = dataURLFilter;
            ParentFilterId = parentFilterId;
        }
    }
}
