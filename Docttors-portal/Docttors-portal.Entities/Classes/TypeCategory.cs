using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("TypeCategory")]
    public class TypeCategory : BaseClass
    {
        [Key]
        public int TypeCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
