using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class TanyaJawabModel
    {
        public int? intJawabanID { get; set; }
        public int? intPertanyaanID { get; set; }
        public int? intOrderJawaban { get; set; }
        public string? txtPertanyaan { get; set; }
        public string? txtJawaban { get; set; }
        public string? isMandatory { get; set; }
        public bool? bitMandatory { get; set; }
    }
}
