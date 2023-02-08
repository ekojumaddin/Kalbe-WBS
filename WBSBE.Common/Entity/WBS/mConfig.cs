using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intConfig { get; set; }

        [StringLength(50)]
        public string txtName { get; set; }

        [StringLength(50)]
        public string txtType { get; set; }

        [StringLength(255)]
        public string txtValue { get; set; }

        public bool bitActive { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtmInserted { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
        public DateTime? dtmUpdated { get; set; }
    }
}
