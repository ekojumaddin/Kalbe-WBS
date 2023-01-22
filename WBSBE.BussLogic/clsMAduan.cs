using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;
using WBSBE.Common.Library;
using WBSBE.Common.Library.Interface;
using WBSBE.Common.Model;
using WBSBE.Common.Model.Custom;
using WBSBE.DAL.Context;

namespace WBSBE.BussLogic
{
    public class clsMAduan : IOperation<AduanModel, WBSDBContext>
    {
        public string Delete(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Delete(string paramTxtId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<AduanModel> GetAllData(WBSDBContext context)
        {
            List<AduanModel> listAduan = new List<AduanModel>();
            var query = (from a in context.mAduan
                        join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan
                        join l in context.mAttachment on a.txtNomorID equals l.txtNomorAduan
                        where a.bitActive == true
                        orderby a.txtNomorID
                        select new
                        {
                            Nomor = a.txtNomorID,
                            Status = a.txtStatus,
                            Pelapor = a.txtPelapor,
                            NIK = a.txtNIK,
                            Nama = a.txtNama,
                            Tlp = a.txtTlp,
                            Email = a.txtEmail,
                            Pertanyaan1 = j.txtPertanyaan1,
                            Pertanyaan2 = j.txtPertanyaan2,
                            Pertanyaan3 = j.txtPertanyaan3,
                            Pertanyaan4 = j.txtPertanyaan4
                        }).ToList();

            foreach (var aduan in query)
            {
                listAduan.Add(new AduanModel()
                {
                    txtNomorID = aduan.Nomor,
                    txtStatus = aduan.Status,
                    txtPelapor = aduan.Pelapor,
                    txtNIK = aduan.NIK,
                    txtNama = aduan.Nama,
                    txtTlp = aduan.Tlp,
                    txtEmail = aduan.Email,
                    txtPertanyaan1 = aduan.Pertanyaan1,
                    txtPertanyaan2 = aduan.Pertanyaan2,
                    txtPertanyaan3 = aduan.Pertanyaan3,
                    txtPertanyaan4 = aduan.Pertanyaan4
                });
            }

            return listAduan;
        }

        public List<AduanModel> GetAllData()
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    return GetAllData(context);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClsPagingModelResponse GetAllDataActiveTable(ClsPagingModelRequest paramData)
        {
            throw new NotImplementedException();
        }

        public ClsPagingModelResponse GetAllDataDataTable(ClsPagingModelRequest paramData)
        {
            throw new NotImplementedException();
        }

        public AduanModel GetDataById(int paramId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public AduanModel GetDataById(string paramTxtId)
        {
            using (var context = new WBSDBContext())
            {
                var query = (from a in context.mAduan
                                 join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan
                                 join l in context.mAttachment on a.txtNomorID equals l.txtNomorAduan
                                 where a.txtNomorID == paramTxtId
                                 select new
                                 {
                                     Nomor = a.txtNomorID,
                                     Status = a.txtStatus,
                                     Pelapor = a.txtPelapor,
                                     NIK = a.txtNIK,
                                     Nama = a.txtNama,
                                     Tlp = a.txtTlp,
                                     Email = a.txtEmail,
                                     Pertanyaan1 = j.txtPertanyaan1,
                                     Pertanyaan2 = j.txtPertanyaan2,
                                     Pertanyaan3 = j.txtPertanyaan3,
                                     Pertanyaan4 = j.txtPertanyaan4
                                 }).ToList();

                var aduan = query.FirstOrDefault();

                AduanModel model = new AduanModel();
                if (query.Count > 0)
                {
                    model.txtNomorID = aduan.Nomor;
                    model.txtStatus = aduan.Status;
                    model.txtPelapor = aduan.Pelapor;
                    model.txtNIK = aduan.NIK;
                    model.txtNama = aduan.Nama;
                    model.txtTlp = aduan.Tlp;
                    model.txtEmail = aduan.Email;
                    model.txtPertanyaan1 = aduan.Pertanyaan1;
                    model.txtPertanyaan2 = aduan.Pertanyaan2;
                    model.txtPertanyaan3 = aduan.Pertanyaan3;
                    model.txtPertanyaan4 = aduan.Pertanyaan4;
                }
                else 
                {
                    model.message = "Data tidak di temukan";
                }

                return model;
            }
        }

        public AduanModel GetVerify(string NIK, string tglLahir)
        {
            throw new NotImplementedException();
        }

        public string Insert(AduanModel paramData)
        {
            using (var context = new WBSDBContext())
            {
                try
                {
                    mAduan aduan = new mAduan();
                    aduan.txtTlp = paramData.txtTlp;
                    aduan.txtNIK = paramData.txtNIK;
                    aduan.txtNama = paramData.txtNama;
                    aduan.txtEmail = paramData.txtEmail;
                    aduan.txtPelapor = paramData.txtPelapor;
                    aduan.txtStatus = "Open";
                    aduan.dtmInserted = DateTime.UtcNow;
                    aduan.txtInsertedBy = "Manual";
                    aduan.bitActive = true;

                    context.mAduan.Add(aduan);
                    context.SaveChanges();

                    mJawabPertanyaan jawaban = new mJawabPertanyaan();
                    jawaban.txtNomorAduan = aduan.txtNomorID;
                    jawaban.txtPertanyaan1 = paramData.txtPertanyaan1;
                    jawaban.txtPertanyaan2 = paramData.txtPertanyaan2;
                    jawaban.txtPertanyaan3 = paramData.txtPertanyaan3;
                    jawaban.txtPertanyaan4 = paramData.txtPertanyaan4;
                    jawaban.dtmInserted = DateTime.UtcNow;
                    jawaban.txtInsertedBy = "Manual";

                    context.mJawabPertanyaan.Add(jawaban);
                    context.SaveChanges();

                    foreach (IFormFile file in paramData.fileData)
                    {
                        mAttachment attachment = new mAttachment();
                        FileInfo fi = new FileInfo(file.FileName);                       
                        var fileExt = fi.Extension;
                        var fileName = Guid.NewGuid().ToString() + fileExt;
                        attachment.txtEncryptedName = clsCommonFunction.saveAttachment(file, ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest, fileName);

                        string allowedExt = context.mConfig.Where(c => c.txtType == "BuktiPendukung" && c.txtName == "AllowedBuktiPendukungExt" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
                        int alloweedMaxSizeInKB = int.Parse(context.mConfig.Where(c => c.txtType == "BuktiPendukung" && c.txtName == "AllowedBuktiPendukungMaxSize" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault());

                        List<string> extension = allowedExt.Split(',').ToList();
                        if (!extension.Exists(x=> x == fileExt))
                        {
                            return ResponseHandler.SendResponse("Tipe file tidak dapat di-upload");
                        }

                        var fileSize = file.Length;
                        string fileBytesToKB = fileSize.ToSize(ClsAttachment.SizeUnits.KB);
                        decimal value;
                        Decimal.TryParse(fileBytesToKB, out value);

                        int fileToInt = Convert.ToInt32(value);

                        if (fileToInt > alloweedMaxSizeInKB * 1024)
                        {
                            return ResponseHandler.SendResponse("Ukuran file terlalu besar");
                        }

                        attachment.txtNomorAduan = aduan.txtNomorID;
                        attachment.bitActive = true;
                        attachment.dtmInserted = DateTime.UtcNow;
                        attachment.txtInsertedBy = "Manual"; //will be change to user login
                        attachment.txtFileName = file.FileName;
                        attachment.txtFileSize = fileToInt;
                        attachment.txtType = fileExt;

                        var pathAttachment = ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest;
                        pathAttachment = pathAttachment.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
                        attachment.txtFilePath = ClsGlobalClass.GetRootPath + pathAttachment + fileName;

                        context.mAttachment.Add(attachment);
                        context.SaveChanges();
                    }

                    #region sendingEmail  
                    //string subjectMailComplainer = context.mConfig.Where(c => c.txtType == "ComplaintReceived" && c.txtName == "MailSubject" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();
                    //string fromMailComplainer = context.mConfig.Where(c => c.txtType == "ComplaintReceived" && c.txtName == "MailFrom" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();
                    //string bodyMailComplainer = context.mConfig.Where(c => c.txtType == "ComplaintReceived" && c.txtName == "MailBody" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();

                    //bodyMailComplainer = bodyMailComplainer.Replace("##FULLNAME##", paramData.txtNama).Replace("##NOMORADUAN##", aduan.txtNomorID);

                    //cstmMailModel mailModelComplainer = new cstmMailModel();
                    //mailModelComplainer.txtSubject = subjectMailComplainer;
                    //mailModelComplainer.txtBody = bodyMailComplainer;
                    //mailModelComplainer.txtFrom = fromMailComplainer;
                    //mailModelComplainer.txtTo = paramData.txtEmail;

                    //string subjectMailCommmittee = context.mConfig.Where(c => c.txtType == "ComplaintIn" && c.txtName == "MailSubject" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();
                    //string fromMailCommmittee = context.mConfig.Where(c => c.txtType == "ComplaintIn" && c.txtName == "MailFrom" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();
                    //string bodyMailCommittee = context.mConfig.Where(c => c.txtType == "ComplaintIn" && c.txtName == "MailBody" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();                    
                    //string toMailCommittee = context.mConfig.Where(c => c.txtType == "ComplaintIn" && c.txtName == "MailTo" && c.bitActive == true)
                    //                                .Select(c => c.txtValue).FirstOrDefault();

                    //bodyMailCommittee = bodyMailCommittee.Replace("##FULLNAME##", toMailCommittee).Replace("##NOMORADUAN##", aduan.txtNomorID);

                    //cstmMailModel mailModelCommitee = new cstmMailModel();
                    //mailModelCommitee.txtSubject = subjectMailCommmittee;
                    //mailModelCommitee.txtBody = bodyMailCommittee;
                    //mailModelCommitee.txtFrom = fromMailCommmittee;
                    //mailModelCommitee.txtTo = paramData.txtEmail;
                    //mailModelCommitee.listAttachment = paramData.fileData;

                    //try
                    //{
                    //    clsCommonFunction.sendEmail(mailModelComplainer);
                    //    clsCommonFunction.sendEmail(mailModelCommitee);

                    //    aduan.bitSentMail = true;
                    //    context.SaveChanges();
                    //}
                    //catch (Exception ex)
                    //{
                    //    return ResponseHandler.SendResponse(ex.Message);
                    //}                   

                    //aduan.bitSentMail = true;
                    //context.SaveChanges();
                    #endregion 

                    return ResponseHandler.SendResponse("Data berhasil di simpan");
                }
                catch (Exception ex)
                {
                    return ResponseHandler.SendResponse(ex.Message);
                }
            }
        }

        public string Insert(AduanModel paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public string Update(AduanModel paramData)
        {
            throw new NotImplementedException();
        }

        public string Update(AduanModel paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
