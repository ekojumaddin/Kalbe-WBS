using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class ResultInvestigationModel
    {
        [StringLength(50)]
        public string? txtNomorID { get; set; }

        public string? ExecutiveSummary { get; set; }

        public string? Notes { get; set; }

        public string? statusLaporan { get; set; }

        public List<AttachmentResultModel>? listDocument { get; set; }

        //public List<IFormFile> listDocument { get; set; }
        public string? message { get; set; }
    }
}
