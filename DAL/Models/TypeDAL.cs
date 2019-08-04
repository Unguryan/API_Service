using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class TypeDAL
    {
        [Key]
        public int Id { get; set; }

        public string NameType { get; set; }

        public virtual IEnumerable<StringAttribute> Attributes { get;set;}

        public virtual IEnumerable<StringRequired> Required { get; set; }

        [NotMapped]
        public virtual Dictionary<string, string> MaxLength { get; set; }

        public string MaxLengthJSON
        {
            get
            {
                return JsonConvert.SerializeObject(MaxLength);
            }
            set
            {
                MaxLength = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }

        [NotMapped]
        public virtual Dictionary<string, string> MinLength { get; set; }

        public string MinLengthJSON
        {
            get
            {
                return JsonConvert.SerializeObject(MinLength);
            }
            set
            {
                MinLength = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
            }
        }
    }
}
