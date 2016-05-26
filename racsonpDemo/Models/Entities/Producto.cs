using System.Web.SessionState;
using Newtonsoft.Json;

namespace racsonpDemo.Models.Entities
{
    public class Producto
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public string Tienda { get; set; }

    }
}