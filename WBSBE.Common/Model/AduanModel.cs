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

        //[StringLength(255)]
        //public string txtPertanyaan1 { get; set; }

        //[StringLength(255)]
        //public string txtPertanyaan2 { get; set; }

        //[StringLength(255)]
        //public string txtPertanyaan3 { get; set; }

        //[StringLength(255)]
        //public string txtPertanyaan4 { get; set; }

        //public int intAttachmentID { get; set; }

        //[StringLength(50)]
        //public string txtType { get; set; }

        //[StringLength(255)]
        //public string txtFileName { get; set; }

        //[StringLength(255)]
        //public string txtEncryptedName { get; set; }

        //public decimal? txtFileSize { get; set; }

        //[StringLength(255)]
        //public string txtFilePath { get; set; }

        ////public List<IFormFile> fileData { get; set; }
        //public IEnumerable<IFormFile> fileData { get; set; }
        //public string? message { get; set; }

        //public List<string> fileName { get; set; }

        ////public List<IFormFile> fileData { get; set; }
        //public List<TanyaJawabModel>? listJawaban { get; set; } = new List<TanyaJawabModel>();


        //public List<TanyaJawabModel> listTanyaJawab { get; set; }

        //[Timestamp()]
        //public DateTime? dtmInserted { get; set; }

        [StringLength(255)]
        public string? txtJawabPertanyaan1 { get; set; }

        //[StringLength(255)]
        //public string? txtPertanyaan2 { get; set; }

        //[StringLength(255)]
        //public string? txtPertanyaan3 { get; set; }

        //[StringLength(255)]
        //public string? txtPertanyaan4 { get; set; }

        public List<TanyaJawabModel>? listJawaban { get; set; } = new List<TanyaJawabModel>();

        public List<string>? fileName { get; set; }

        public IEnumerable<IFormFile> fileData { get; set; }

        public List<TanyaJawabModel>? listTanyaJawab { get; set; }

        //public List<Byte[]> fileByte { get; set; }

        //public List<ByteArrayContent> fileByteArrayContent { get; set; }

        //public List<MemoryStream> fileMemoryStream { get; set; }

        //public List<FileStream> fileStream { get; set; }

        //public List<StreamReader> streamReader { get; set; }

        //public List<FileContentResult> fileContent { get; set; }
        public string? message { get; set; }
    }
}
