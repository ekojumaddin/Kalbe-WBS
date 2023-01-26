using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        LoggerManager logger = new LoggerManager();
        public string Delete(string paramTxtId)
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    return Delete(paramTxtId, context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public string Delete(string paramTxtId, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var dt = context.mAduan.Where(x => x.txtNomorID == paramTxtId).FirstOrDefault();

                    dt.answerForQuestion = context.mJawabPertanyaan.Where(x => x.txtNomorAduan.txtNomorID == paramTxtId).ToList();
                    dt.listAttachments = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramTxtId).ToList();
                    dt.bitActive = false;
                    dt.dtmUpdated = DateTime.UtcNow;
                    dt.txtUpdatedBy = "Manual";

                    foreach (var dtPertanyaan in dt.answerForQuestion)
                    {
                        dtPertanyaan.bitActive = false;
                        dtPertanyaan.dtmUpdated = DateTime.UtcNow;
                        dtPertanyaan.txtUpdatedBy = "Manual";
                    }

                    foreach (var dtAttachment in dt.listAttachments)
                    {
                        dtAttachment.bitActive = false;
                        dtAttachment.dtmUpdated = DateTime.UtcNow;
                        dtAttachment.txtUpdatedBy = "Manual";
                    }

                    context.Update(dt);
                    context.SaveChanges();
                    transaction.Commit();
                    return paramTxtId;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    transaction.Rollback();                    
                    throw;
                }
            }
        }

        public List<AduanModel> GetAllData(WBSDBContext context)
        {
            List<AduanModel> listAduan = new List<AduanModel>();
            var query = (from a in context.mAduan
                        join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan.txtNomorID       
                        join l in context.mAttachment on a.txtNomorID equals l.mAduan.txtNomorID
                        where a.bitActive == true && j.bitActive == true && l.bitActive == true
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
                logger.LogError(ex.Message);
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
                             join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan.txtNomorID
                             join l in context.mAttachment on a.txtNomorID equals l.mAduan.txtNomorID
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
            try
            {
                using (var context = new WBSDBContext())
                {
                    return Insert(paramData, context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public string Insert(AduanModel paramData, WBSDBContext context)
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

                mJawabPertanyaan jawaban = new mJawabPertanyaan();
                jawaban.txtPertanyaan1 = paramData.txtPertanyaan1;
                jawaban.txtPertanyaan2 = paramData.txtPertanyaan2;
                jawaban.txtPertanyaan3 = paramData.txtPertanyaan3;
                jawaban.txtPertanyaan4 = paramData.txtPertanyaan4;
                jawaban.dtmInserted = DateTime.UtcNow;
                jawaban.txtInsertedBy = "Manual";
                jawaban.bitActive = true;

                aduan.answerForQuestion.Add(jawaban);

                foreach (IFormFile file in paramData.fileData)
                {
                    mAttachment attachment = new mAttachment();
                    string message = AttachmentValidation(file, context, attachment);

                    if (String.IsNullOrEmpty(message))
                    {
                        InsertAttachment(attachment, file);

                        attachment.bitActive = true;
                        attachment.txtFileName = file.FileName;
                        attachment.dtmInserted = DateTime.UtcNow;
                        attachment.txtInsertedBy = "Manual"; //will be change to user login                                    

                        aduan.listAttachments.Add(attachment);
                    }
                    else
                    {
                        return ResponseHandler.SendResponse(message);
                    }
                }

                context.mAduan.Add(aduan);
                context.SaveChanges();

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

        public string Update(AduanModel paramData)
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    return Update(paramData, context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public string Update(AduanModel paramData, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existAduan = context.mAduan.Where(x => x.txtNomorID == paramData.txtNomorID && x.bitActive == true).FirstOrDefault();
                    existAduan.txtTlp = paramData.txtTlp;
                    existAduan.txtNIK = paramData.txtNIK;
                    existAduan.txtNama = paramData.txtNama;
                    existAduan.txtEmail = paramData.txtEmail;
                    existAduan.txtPelapor = paramData.txtPelapor;
                    existAduan.dtmUpdated = DateTime.UtcNow;
                    existAduan.txtUpdatedBy = "Manual"; //will be change to user login

                    var existJawaban = context.mJawabPertanyaan.Where(x => x.txtNomorAduan.txtNomorID == paramData.txtNomorID && x.bitActive == true).FirstOrDefault();
                    existJawaban.txtPertanyaan1 = paramData.txtPertanyaan1;
                    existJawaban.txtPertanyaan2 = paramData.txtPertanyaan2;
                    existJawaban.txtPertanyaan3 = paramData.txtPertanyaan3;
                    existJawaban.txtPertanyaan4 = paramData.txtPertanyaan4;
                    existJawaban.dtmUpdated = DateTime.UtcNow;
                    existJawaban.txtUpdatedBy = "Manual"; //will be change to user login
                    existAduan.answerForQuestion.Add(existJawaban);

                    if (paramData.fileData != null)
                    {
                        foreach (IFormFile file in paramData.fileData)
                        {
                            var existAttachment = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramData.txtNomorID && x.txtFileName == file.FileName && x.bitActive == true).FirstOrDefault();
                            var listAttachment = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramData.txtNomorID && x.bitActive == true).Select(x => x.txtFileName).ToList();

                            if (existAttachment != null && listAttachment.Exists(x => x == file.FileName))
                            {

                                foreach (var attach in listAttachment)
                                {
                                    if (attach == file.FileName)
                                    {
                                        string message = AttachmentValidation(file, context, existAttachment);

                                        if (String.IsNullOrEmpty(message))
                                        {
                                            FileInfo fi = new FileInfo(file.FileName);
                                            var fileExt = fi.Extension;
                                            existAttachment.txtEncryptedName = clsCommonFunction.saveAttachment(file, ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest, existAttachment.txtEncryptedName);
                                            var pathAttachment = ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest;
                                            pathAttachment = pathAttachment.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
                                            existAttachment.txtFilePath = ClsGlobalClass.GetRootPath + pathAttachment + existAttachment.txtFileName;
                                            existAttachment.dtmUpdated = DateTime.UtcNow;
                                            existAttachment.txtUpdatedBy = "Manual"; //will be change to user login                         

                                            existAduan.listAttachments.Add(existAttachment);
                                        }
                                        else
                                        {
                                            return ResponseHandler.SendResponse(message);
                                        }
                                    }
                                    else
                                    {
                                        var fullPath = Path.GetFullPath(ClsGlobalClass.GetRootPath + ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest);
                                        var nonActivateAttachment = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramData.txtNomorID && x.bitActive == true && x.txtFileName == attach).FirstOrDefault();
                                        clsCommonFunction.checkAndDeleteAttacment(fullPath, nonActivateAttachment.txtEncryptedName);

                                        nonActivateAttachment.bitActive = false;

                                        existAduan.listAttachments.Add(nonActivateAttachment);
                                    }
                                }
                            }
                            else
                            {
                                mAttachment attachment = new mAttachment();
                                string message = AttachmentValidation(file, context, attachment);

                                if (String.IsNullOrEmpty(message))
                                {
                                    InsertAttachment(attachment, file);

                                    attachment.bitActive = true;
                                    attachment.txtFileName = file.FileName;
                                    attachment.dtmInserted = DateTime.UtcNow;
                                    attachment.txtInsertedBy = "Manual"; //will be change to user login                                    

                                    existAduan.listAttachments.Add(attachment);
                                }
                                else
                                {
                                    return ResponseHandler.SendResponse(message);
                                }
                            }
                        }
                    }

                    existAduan.dtmUpdated = DateTime.UtcNow;
                    existAduan.txtUpdatedBy = "Manual"; //will be change to user login
                    context.Update(existAduan);
                    context.SaveChanges();
                    transaction.Commit();

                    return ResponseHandler.SendResponse("Data berhasil diubah");
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public string AttachmentValidation(IFormFile file, WBSDBContext context, mAttachment attachment)
        {
            string message = "";
            if (!AllowedExtensionAttachment(context, file, attachment))
            {
                message = "Tipe file tidak dapat di-upload";
            }

            if (!MaxSizeAttachment(context, file, attachment))
            {
                message = "Ukuran file terlalu besar";
            }

            return message;
        }

        public bool AllowedExtensionAttachment(WBSDBContext context, IFormFile file, mAttachment attachment)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var fileExt = fi.Extension;

            string allowedExt = context.mConfig.Where(c => c.txtType == "BuktiPendukung" && c.txtName == "AllowedBuktiPendukungExt" && c.bitActive == true)
                                .Select(c => c.txtValue).FirstOrDefault();

            List<string> extension = allowedExt.Split(',').ToList();
            if (!extension.Exists(x => x == fileExt))
            {
                return false;
            }

            attachment.txtType = fileExt;

            return true;
        }

        public bool MaxSizeAttachment(WBSDBContext context, IFormFile file, mAttachment attachment)
        {
            int alloweedMaxSizeInKB = int.Parse(context.mConfig.Where(c => c.txtType == "BuktiPendukung" && c.txtName == "AllowedBuktiPendukungMaxSize" && c.bitActive == true)
                                .Select(c => c.txtValue).FirstOrDefault());

            var fileSize = file.Length;
            string fileBytesToKB = fileSize.ToSize(ClsAttachment.SizeUnits.KB);
            decimal value;
            Decimal.TryParse(fileBytesToKB, out value);

            int fileToInt = Convert.ToInt32(value);

            if (fileToInt > alloweedMaxSizeInKB * 1024)
            {
                return false;
            }

            attachment.txtFileSize = fileToInt;

            return true;
        }

        public void InsertAttachment(mAttachment attachment, IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var fileExt = fi.Extension;
            var fileName = Guid.NewGuid().ToString() + fileExt;
            attachment.txtEncryptedName = clsCommonFunction.saveAttachment(file, ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest, fileName);
            var pathAttachment = ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest;
            pathAttachment = pathAttachment.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
            attachment.txtFilePath = ClsGlobalClass.GetRootPath + pathAttachment + fileName;
        }

        public List<AduanModel> sortingData(string sortOrder)
        {
            using (var context = new WBSDBContext())
            {
                List<AduanModel> listAduan = new List<AduanModel>();
                var query = from a in context.mAduan
                            join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan.txtNomorID
                            where a.bitActive == true && j.bitActive == true
                            orderby a.txtNomorID
                            select new
                            {
                                Nomor = a.txtNomorID,
                                Status = a.txtStatus,
                                Pertanyaan1 = j.txtPertanyaan1
                            };

                switch (sortOrder)
                {
                    case "nomor":
                        query = query.OrderByDescending(s => s.Nomor);
                        break;
                    case "pertanyaan":
                        query = query.OrderBy(s => s.Status);
                        break;
                    case "status":
                        query = query.OrderByDescending(s => s.Pertanyaan1);
                        break;
                    default:
                        query = query.OrderBy(s => s.Nomor);
                        break;
                }

                foreach (var aduan in query)
                {
                    listAduan.Add(new AduanModel()
                    {
                        txtNomorID = aduan.Nomor,
                        txtStatus = aduan.Status,
                        txtPertanyaan1 = aduan.Pertanyaan1
                    });
                }

                return listAduan;
            }
        }

        public List<AduanModel> searchingData(string sortOrder, string searchString)
        {
            using (var context = new WBSDBContext())
            {
                List<AduanModel> listAduan = new List<AduanModel>();

                var query = from a in context.mAduan
                            join j in context.mJawabPertanyaan on a.txtNomorID equals j.txtNomorAduan.txtNomorID
                            where a.bitActive == true && j.bitActive == true
                            orderby a.txtNomorID
                            select new
                            {
                                Nomor = a.txtNomorID,
                                Status = a.txtStatus,
                                Pertanyaan1 = j.txtPertanyaan1
                            };

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(s => s.Nomor.Contains(searchString) || s.Status.Contains(searchString) || s.Pertanyaan1.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "nomor":
                        query = query.OrderByDescending(s => s.Nomor);
                        break;
                    case "pertanyaan":
                        query = query.OrderBy(s => s.Status);
                        break;
                    case "status":
                        query = query.OrderByDescending(s => s.Pertanyaan1);
                        break;
                    default:
                        query = query.OrderBy(s => s.Nomor);
                        break;
                }

                foreach (var aduan in query)
                {
                    listAduan.Add(new AduanModel()
                    {
                        txtNomorID = aduan.Nomor,
                        txtStatus = aduan.Status,
                        txtPertanyaan1 = aduan.Pertanyaan1
                    });
                }

                return listAduan;
            }
        }
    }
}
