using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class AduanModel
    {
        [StringLength(50)]
        public string? txtNomorID { get; set; }

        [StringLength(25)]
        public string? txtStatus { get; set; }

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

        [StringLength(255)]
        public string txtPertanyaan1 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan2 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan3 { get; set; }

        [StringLength(255)]
        public string txtPertanyaan4 { get; set; }

        public List<string> fileName { get; set; }

        public List<IFormFile> fileData { get; set; }

        public List<TanyaJawabModel> listTanyaJawab { get; set; }
        public string? message { get; set; }
    }
}
