using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctoreNews")]
    public class DoctoreNews : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
