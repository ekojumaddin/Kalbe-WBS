using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mJawaban
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intJawabanID { get; set; }

        [StringLength(255)]
        public string txtJawaban { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtmInserted { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
        public DateTime? dtmUpdated { get; set; }
        public bool bitActive { get; set; }

        public int intPertanyaanID { get; set; }

        public virtual mAduan txtNomorAduan { get; set; }
    }
}
