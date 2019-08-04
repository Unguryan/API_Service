using Newtonsoft.Json;
using System.Collections.Generic;


namespace BLL.Models
{
    public class EntityBLL
    {
        public int Id { get; set; }

        public string NameEntity { get; set; }

        [JsonProperty("attributes")] public Dictionary<string, object> Attributes { get; set; }
    }
}
