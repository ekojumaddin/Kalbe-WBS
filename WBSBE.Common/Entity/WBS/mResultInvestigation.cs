using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mResultInvestigation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intResultInvestigationID { get; set; }

        [StringLength(255)]
        public string txtExecutive { get; set; }

        [StringLength(255)]
        public string txtNote { get; set; }
        public bool? bitActive { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtInserted { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
        public DateTime? dtUpdated { get; set; }

        [StringLength(50)]
        public string txtNomorID { get; set; }
        public bool bitSubmit { get; set; }
        public bool bitSentMail { get; set; }
    }
}
