using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class AttributeDAL
    {
        [Key]
        public int Id { get;set;}

        [NotMapped]
        public object Item { get; set; }

        public string ItemJSON
        {
            get
            {
                return JsonConvert.SerializeObject(Item);
            }
            set
            {
                Item = JsonConvert.DeserializeObject(value);
            }
        }

    }
}
