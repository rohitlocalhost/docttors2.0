using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctorOnlineFees")]
    public class DoctorOnlineFees : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal? AskDoctor { get; set; }
        public decimal? VideoEmail { get; set; }
        public decimal? face2face { get; set; }
        public decimal? RxRefill { get; set; }
    }
}
