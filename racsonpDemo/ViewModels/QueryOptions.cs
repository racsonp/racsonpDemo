using Newtonsoft.Json;

namespace racsonpDemo.ViewModels
{

    //  http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
    //para agregra busqueda por string
    public class QueryOptions
    {

        public QueryOptions()
        {
            CurrentPage = 1;
            PageSize = 30;
            //TODO
            SortField = "Id";
            // SortField = "Id";
            // SortField = campo;
            SortOrder = ViewModels.SortOrder.ASC.ToString();
        }

        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "sortField")]
        public string SortField { get; set; }

        [JsonProperty(PropertyName = "sortOrder")]
        public string SortOrder { get; set; }

        [JsonIgnore]
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}",
                    SortField, SortOrder);
            }
        }
    }
}