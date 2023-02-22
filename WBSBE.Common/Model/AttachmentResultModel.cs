using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class AttachmentResultModel
    {
        public string? nomor { get; set; }
        public string? fileDescription { get; set; }
        public IFormFile listAttachment { get; set; }
    }
}
