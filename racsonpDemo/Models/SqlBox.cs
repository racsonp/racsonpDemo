using System.ComponentModel.DataAnnotations;
using System.Data;

using Newtonsoft.Json;

namespace racsonpDemo.Models
{
    public class SqlBox
    {

        public SqlBox()
        {
            DataTable = new DataTable();
            Format = 1;
        }


        [JsonProperty(PropertyName = "successful")]
        public bool Successful { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }
        // public int Top { get; set; }

        [JsonProperty(PropertyName = "format")]
        public int Format { get; set; }

        [JsonProperty(PropertyName = "dataTable")]
        public DataTable DataTable { get; set; }


    }
}

