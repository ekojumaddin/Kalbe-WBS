using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mJawabPertanyaan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intJwbPertanyaanID { get; set; }

        [StringLength(255)]
        public string txtPertanyaan1 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan2 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan3 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan4 { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtmInserted { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
        public DateTime? dtmUpdated { get; set; }

        public bool bitActive { get; set; }

        public virtual mAduan txtNomorAduan { get; set; }
    }
}
