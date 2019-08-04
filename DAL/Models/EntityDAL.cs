using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class EntityDAL
    {
        [Key]
        public int Id { get; set; }

        public string NameEntity { get; set; }

        [NotMapped]
        public virtual Dictionary<string, AttributeDAL> Attributes { get; set; }

        public string AttributesJSON
        {
            get
            {
                return JsonConvert.SerializeObject(Attributes);
            }
            set
            {
                Attributes = JsonConvert.DeserializeObject<Dictionary<string, AttributeDAL>>(value);
            }
        }

    }
}
