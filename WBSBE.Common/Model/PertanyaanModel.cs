using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class PertanyaanModel
    {
        public int? intPertanyaanID { get; set; }

        [StringLength(255)]
        public string? txtPertanyaan { get; set; }

        public int? intOrderPertanyaan { get; set; }

        public bool? bitMandatory { get; set; }

        public string? isMandatory { get; set; }

        public string? isActive { get; set; }

        public List<TanyaJawabModel>? listPertanyaan { get; set; } = new List<TanyaJawabModel>();
    }
}
