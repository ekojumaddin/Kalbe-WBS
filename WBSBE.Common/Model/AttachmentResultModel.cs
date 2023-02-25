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
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? dtmInserted { get; set; }
        public string? txtInsertedBy { get; set; }
        public IFormFile? listAttachment { get; set; }
    }
}
