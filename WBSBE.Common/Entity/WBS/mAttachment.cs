using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int intAttachmentID { get; set; }

        [StringLength(50)]
        public string txtNomorAduan { get; set; }

        [StringLength(50)]
        public string txtType { get; set; }

        [StringLength(255)]
        public string txtFileName { get; set; }

        [StringLength(255)]
        public string txtEncryptedName { get; set; }

        public decimal? txtFileSize { get; set; }

        [StringLength(255)]
        public string txtFilePath { get; set; }
        public bool bitActive { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }
        public DateTime? dtmInserted { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }
        public DateTime? dtmUpdated { get; set; }
    }
}
