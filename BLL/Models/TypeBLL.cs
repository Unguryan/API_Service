using DAL.Models;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace BLL.Models
{
    public class TypeBLL
    {
        public int Id { get; set; }

        [JsonProperty("name")] public string NameType { get; set; }
        [JsonProperty("attributes")] public IEnumerable<string> Attributes { get; set; }
        [JsonProperty("required")] public IEnumerable<string> Required { get; set; }
        [JsonProperty("maxLength")] public Dictionary<string, string> MaxLength { get; set; }
        [JsonProperty("minLength")] public Dictionary<string, string> MinLength { get; set; }
    }
}
