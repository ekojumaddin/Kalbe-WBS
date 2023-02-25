using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mHistoryNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intHistoryNote { get; set; }

        [StringLength(255)]
        public string? txtNote { get; set; }

        [StringLength(50)]
        public string txtNomorAduan { get; set; }

        [StringLength(255)]
        public string Action { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtmInserted { get; set; }
    }
}
