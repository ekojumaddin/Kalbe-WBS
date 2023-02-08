using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom
{
    public class cstmMailModel
    {
        public string txtTo { get; set; }
        public string txtFrom { get; set; }
        public string txtSubject { get; set; }
        public string txtBody { get; set; }
        public List<string> listTxtCC { get; set; }
        public List<IFormFile> listAttachment { get; set; }
        public IFormFile attachment { get; set; }
        public string txtSmtp { get; set; }
        public Nullable<bool> bitIsBodyHTML { get; set; }
    }
}
