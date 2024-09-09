using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("Type")]
    public class Type : BaseClass
    {
        [Key]
        public int TypeId { get; set; }
        public int TypeCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
