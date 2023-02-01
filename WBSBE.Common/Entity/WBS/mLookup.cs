using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mLookup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intLookupID { get; set; }

        [StringLength(50)]
        public string txtType { get; set; }

        [StringLength(50)]
        public string txtName { get; set; }

        public int? intValue { get; set; }
        public int? intOrderNo { get; set; }
        public bool bitActive { get; set; }
        public DateTime? dtmInserted { get; set; }
        public DateTime? dtmUpdated { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
    }
}
