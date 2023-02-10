using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mAduan
    {
        public mAduan()
        {
            listAttachments = new List<mAttachment>();
            listJawaban = new List<mJawaban>();
        }

        [Key]
        [StringLength(50)]
        public string txtNomorID { get; set; }

        [StringLength(25)]
        public string txtStatus { get; set; }

        [StringLength(50)]
        public string txtPelapor { get; set; }

        [StringLength(50)]
        public string? txtNIK { get; set; }

        [StringLength(50)]
        public string txtNama { get; set; }

        [StringLength(50)]
        public string? txtTlp { get; set; }

        [StringLength(50)]
        public string txtEmail { get; set; }
        public bool bitSentMail { get; set; }
        public bool bitActive { get; set; }
        public DateTime? dtmInserted { get; set; }
        public DateTime? dtmUpdated { get; set; }

        [StringLength(50)]
        public string? txtInsertedBy { get; set; }

        [StringLength(50)]
        public string? txtUpdatedBy { get; set; }

        public List<mAttachment> listAttachments { get; set; }

        public List<mJawaban> listJawaban { get; set; }
    }
}
