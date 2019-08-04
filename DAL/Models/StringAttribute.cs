using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models
{
    
    public class StringAttribute
    {
        [Key]
        public int Id { get; set; }

        public string Attribute { get; set; }

        [ForeignKey("TypeDAL")]
        public int TypeDALId { get; set; }

        public virtual TypeDAL TypeDAL { get; set; }

    }
}
