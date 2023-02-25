using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
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
    public class clsMResultInvestigation : IOperation<ResultInvestigationModel, WBSDBContext>
    {
        #region Property 
        LoggerManager logger = new LoggerManager();
        #endregion

        public string Delete(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Delete(string paramTxtId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<ResultInvestigationModel> GetAllData(WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<ResultInvestigationModel> GetAllData()
        {
            throw new NotImplementedException();
        }

        public ClsPagingModelResponse GetAllDataActiveTable(ClsPagingModelRequest paramData)
        {
            throw new NotImplementedException();
        }

        public ClsPagingModelResponse GetAllDataDataTable(ClsPagingModelRequest paramData)
        {
            throw new NotImplementedException();
        }

        public ResultInvestigationModel GetDataById(int paramId, WBSDBContext context)
        {
            ResultInvestigationModel result = new ResultInvestigationModel();
            result.listDocument = new();

            var existsNomor = context.mResultInvestigation.Where(n => n.intResultInvestigationID == paramId && n.bitActive == true).FirstOrDefault();

            if (existsNomor != null)
            {

                result.txtNomorID = existsNomor.txtNomorID;
                result.ExecutiveSummary = existsNomor.txtNote;
                if (existsNomor.bitSubmit == true)
                {
                    result.statusLaporan = "Submit";
                }
                else
                {
                    result.statusLaporan = "Saved";
                }

                var existsAttachment = context.mAttachmentResult.Where(a => a.txtNomorID == existsNomor.txtNomorID).ToList();

                if (existsAttachment.Count > 0)
                {
                    foreach (var item in existsAttachment)
                    {
                        AttachmentResultModel attachResult = new AttachmentResultModel();
                        attachResult.FileDescription = item.txtFileDescription;
                        attachResult.FileName = item.txtFileName;
                        attachResult.txtInsertedBy = item.txtInsertedBy;                        

                        if (item.dtmInserted.HasValue)
                        {
                            DateTime createdTime = (DateTime)item.dtmInserted;
                            DateTime dateOnly = createdTime.Date;
                            string dtInserted = dateOnly.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                            attachResult.dtmInserted = dtInserted;
                        }

                        result.listDocument.Add(attachResult);
                    }
                }
            }
            else
            {
                result.message = "Data Hasil Investigasi tidak ditemukan";
            }

            return result;
        }

        public ResultInvestigationModel GetDataById(string paramTxtId)
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    ResultInvestigationModel result = new ResultInvestigationModel();
                    result.listHistory = new();

                    var existsNomor = context.mResultInvestigation.Where(n => n.txtNomorID == paramTxtId && n.bitActive == true).FirstOrDefault();

                    if (existsNomor != null)
                    {
                        result.txtNomorID = existsNomor.txtNomorID;
                        
                        var existNote = context.mHistoryNote.Where(n => n.txtNomorAduan == paramTxtId).OrderByDescending(x => x.dtmInserted).ToList();

                        if (existNote.Count > 0)
                        {
                            foreach (var item in existNote)
                            {
                                HistoryNoteModel notes = new();
                                notes.action = item.Action;

                                result.listHistory.Add(notes);
                            }
                        }
                    }
                    else
                    {
                        result.message = "Data Hasil Investigasi tidak ditemukan";
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }            
        }

        public string Insert(ResultInvestigationModel paramData)
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

        public string Insert(ResultInvestigationModel paramData, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    mResultInvestigation result = new mResultInvestigation();
                    result.txtNomorID = paramData.txtNomorID;
                    result.txtExecutive = paramData.ExecutiveSummary;
                    result.txtNote = paramData.Notes;
                    result.bitSubmit = false;
                    result.bitSentMail = false;
                    result.dtInserted = DateTime.UtcNow;
                    result.txtInsertedBy = "Manual"; //will be change to user login
                    result.bitActive = true;

                    mHistoryNote history1 = new mHistoryNote();
                    history1.Action = "Melakukan penyimpanan hasil laporan";
                    history1.txtNomorAduan = paramData.txtNomorID;
                    history1.txtNote = paramData.Notes;
                    history1.dtmInserted = result.dtInserted;
                    history1.txtInsertedBy = result.txtInsertedBy;

                    context.mHistoryNote.Add(history1);

                    foreach (var file in paramData.listDocument)
                    {
                        mAttachmentResult attachment = new();
                        string message = AttachmentValidation(file.listAttachment, context, attachment);

                        if (String.IsNullOrEmpty(message))
                        {
                            InsertAttachment(attachment, file.listAttachment);
                            attachment.txtNomorID = paramData.txtNomorID;
                            attachment.txtFileDescription = file.FileDescription;
                            attachment.bitActive = true;
                            attachment.txtFileName = file.listAttachment.FileName;
                            attachment.dtmInserted = DateTime.UtcNow;
                            attachment.txtInsertedBy = "Manual"; //will be change to user login

                            context.mAttachmentResult.Add(attachment);

                            mHistoryNote history2 = new();
                            history2.txtNomorAduan = paramData.txtNomorID;
                            history2.Action = "Menambahkan lampiran " + file.listAttachment.FileName;
                            history2.dtmInserted = attachment.dtmInserted;
                            history2.txtInsertedBy = attachment.txtInsertedBy;
                            history2.txtNote = paramData.Notes;

                            context.mHistoryNote.Add(history2);
                        }
                        else
                        {
                            return ResponseHandler.SendResponse(message);
                        }
                    }

                    context.mResultInvestigation.Add(result);
                    context.SaveChanges();
                    transaction.Commit();

                    #region sendingEmail  
                    //SendEmail(context, paramData.txtNomorID, listEmail, listUserName, listRoleName);
                    #endregion

                    return ResponseHandler.SendResponse("Data berhasil di simpan");
                }
                catch (Exception ex)
                {
                    return ResponseHandler.SendResponse(ex.Message);
                }
            }
        }

        public string Update(ResultInvestigationModel paramData)
        {
            throw new NotImplementedException();
        }

        public string Update(ResultInvestigationModel paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public string Submit(ResultInvestigationModel paramData)
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    return Submit(paramData, context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public string Submit(ResultInvestigationModel paramData, WBSDBContext context)
        {
            List<string> listEmail = new List<string>();
            List<string> listUserName = new List<string>();
            List<string> listRoleName = new List<string>();
            List<int> listUserId = new List<int>();
            List<int> listRoleId = new List<int>();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existingInvestigations = context.mSetInvestigation.Where(a => a.txtNomorID == paramData.txtNomorID && a.bitActive == true && a.bitSentMail == false).ToList();

                    foreach (var item in existingInvestigations)
                    {
                        //for list user and role from nomor aduan
                        listUserId.Add((int)item.intUserID);
                        listRoleId.Add((int)item.intRoleID);

                        var user = context.mUser.Find(item.intUserID);
                        var role = context.mRole.Find(item.intRoleID);

                        var email = user.txtEmail;
                        if (email != null)
                        {
                            listEmail.Add(email);
                        }

                        var username = user.txtUserName;
                        if (username != null)
                        {
                            listUserName.Add(username);
                        }

                        var rolename = role.txtRoleName;
                        if (rolename != null)
                        {
                            listRoleName.Add(rolename);
                        }
                    }

                    var existingResultInvestigations = context.mResultInvestigation.Where(a => a.txtNomorID == paramData.txtNomorID && a.bitSubmit == false && a.bitActive == true && a.bitSentMail == false).FirstOrDefault();

                    if (existingResultInvestigations != null)
                    {
                        existingResultInvestigations.txtExecutive = paramData.ExecutiveSummary;
                        existingResultInvestigations.txtNote = paramData.Notes;
                        existingResultInvestigations.bitActive = true;
                        existingResultInvestigations.bitSubmit = true;
                        existingResultInvestigations.dtUpdated = DateTime.UtcNow;
                        existingResultInvestigations.txtUpdatedBy = "Manual"; //will be change to user login

                        mHistoryNote notes = new mHistoryNote();
                        notes.Action = "Melakukan submit hasil laporan";
                        notes.txtNomorAduan = paramData.txtNomorID;
                        notes.txtNote = paramData.Notes;
                        notes.dtmInserted = DateTime.UtcNow;
                        notes.txtInsertedBy = "Manual"; //will be change to user login

                        context.mHistoryNote.Add(notes);

                        if (paramData.listDocument != null)
                        {
                            foreach (var file in paramData.listDocument)
                            {
                                var existAttachment = context.mAttachmentResult.Where(a => a.txtFileName == file.listAttachment.FileName && a.bitActive == true &&
                                                    a.txtNomorID == existingResultInvestigations.txtNomorID).FirstOrDefault();

                                if (existAttachment != null)
                                {
                                    string message = AttachmentValidation(file.listAttachment, context, existAttachment);

                                    if (String.IsNullOrEmpty(message))
                                    {
                                        FileInfo fi = new FileInfo(file.listAttachment.FileName);
                                        var fileExt = fi.Extension;
                                        existAttachment.txtEncryptedName = clsCommonFunction.saveAttachment(file.listAttachment, ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest, existAttachment.txtEncryptedName);
                                        var pathAttachment = ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest;
                                        pathAttachment = pathAttachment.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
                                        existAttachment.txtFilePath = ClsGlobalClass.GetRootPath + pathAttachment + existAttachment.txtFileName;
                                        existAttachment.dtmUpdated = DateTime.UtcNow;
                                        existAttachment.txtUpdatedBy = "Manual"; //will be change to user login

                                        context.mAttachmentResult.Update(existAttachment);

                                        mHistoryNote history = new();
                                        history.txtNomorAduan = paramData.txtNomorID;
                                        history.Action = "Mengubah lampiran " + file.listAttachment.FileName;
                                        history.dtmInserted = existAttachment.dtmUpdated;
                                        history.txtInsertedBy = existAttachment.txtUpdatedBy;
                                        history.txtNote = paramData.Notes;

                                        context.mHistoryNote.Add(history);
                                    }
                                    else
                                    {
                                        return ResponseHandler.SendResponse(message);
                                    }
                                }
                                else 
                                {
                                    mAttachmentResult attachment = new();
                                    string message = AttachmentValidation(file.listAttachment, context, attachment);

                                    if (String.IsNullOrEmpty(message))
                                    {
                                        InsertAttachment(attachment, file.listAttachment);
                                        attachment.txtNomorID = paramData.txtNomorID;
                                        attachment.txtFileDescription = file.FileDescription;
                                        attachment.bitActive = true;
                                        attachment.txtFileName = file.listAttachment.FileName;
                                        attachment.dtmInserted = DateTime.UtcNow;
                                        attachment.txtInsertedBy = "Manual"; //will be change to user login

                                        context.mAttachmentResult.Add(attachment);

                                        mHistoryNote history = new();
                                        history.txtNomorAduan = paramData.txtNomorID;
                                        history.Action = "Menambahkan lampiran " + file.listAttachment.FileName;
                                        history.dtmInserted = attachment.dtmInserted;
                                        history.txtInsertedBy = attachment.txtInsertedBy;
                                        history.txtNote = paramData.Notes;

                                        context.mHistoryNote.Add(history);
                                    }
                                    else
                                    {
                                        return ResponseHandler.SendResponse(message);
                                    }
                                }
                            }
                        }                        

                        #region sendingEmail
                        //SendEmail(context, paramData.txtNomorID, listEmail, listUserName, listRoleName);
                        #endregion

                        existingResultInvestigations.bitSentMail = true;

                        context.mResultInvestigation.Update(existingResultInvestigations);
                    }
                    else
                    {
                        mResultInvestigation result = new mResultInvestigation();
                        result.txtNomorID = paramData.txtNomorID;
                        result.txtExecutive = paramData.ExecutiveSummary;
                        result.txtNote = paramData.Notes;
                        result.bitSubmit = false;
                        result.bitSentMail = false;
                        result.dtInserted = DateTime.UtcNow;
                        result.txtInsertedBy = "Manual"; //will be change to user login
                        result.bitActive = true;

                        mHistoryNote notes = new mHistoryNote();
                        notes.Action = "Melakukan submit hasil laporan";
                        notes.txtNomorAduan = paramData.txtNomorID;
                        notes.txtNote = paramData.Notes;
                        notes.dtmInserted = DateTime.UtcNow;
                        notes.txtInsertedBy = "Manual"; //will be change to user login

                        context.mHistoryNote.Add(notes);

                        foreach (var file in paramData.listDocument)
                        {
                            mAttachmentResult attachment = new();
                            string message = AttachmentValidation(file.listAttachment, context, attachment);

                            if (String.IsNullOrEmpty(message))
                            {
                                InsertAttachment(attachment, file.listAttachment);
                                attachment.txtNomorID = paramData.txtNomorID;
                                attachment.txtFileDescription = file.FileDescription;
                                attachment.bitActive = true;
                                attachment.txtFileName = file.listAttachment.FileName;
                                attachment.dtmInserted = DateTime.UtcNow;
                                attachment.txtInsertedBy = "Manual"; //will be change to user login

                                context.mAttachmentResult.Add(attachment);

                                mHistoryNote history = new();
                                history.txtNomorAduan = paramData.txtNomorID;
                                history.Action = "Menambahkan lampiran " + file.listAttachment.FileName;
                                history.dtmInserted = attachment.dtmInserted;
                                history.txtInsertedBy = attachment.txtInsertedBy;
                                history.txtNote = paramData.Notes;

                                context.mHistoryNote.Add(history);
                            }
                            else
                            {
                                return ResponseHandler.SendResponse(message);
                            }
                        }

                        #region sendingEmail
                        //SendEmail(context, paramData.txtNomorID, listEmail, listUserName, listRoleName);
                        #endregion

                        result.bitSentMail = true;
                        context.Add(result);
                    }

                    var existingAduan = context.mAduan.Where(m => m.txtNomorID == paramData.txtNomorID && m.bitActive == true).FirstOrDefault();
                    if (existingAduan != null)
                    {
                        //update status aduan
                        existingAduan.txtStatus = "Laporan Investigasi diproses";
                        existingAduan.dtmUpdated = DateTime.UtcNow;
                        existingAduan.txtUpdatedBy = "Manual"; //will be change to user login
                        context.Update(existingAduan);
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.StackTrace.ToString());
                    return ResponseHandler.SendResponse(ex.Message);
                }

                return ResponseHandler.SendResponse("Data berhasil di submit");
            }
        }

        #region AddAttachment
        public string AddAttachment(ResultInvestigationModel paramData)
        {
            using (var context = new WBSDBContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {                    
                        var resultInvestigation = context.mResultInvestigation.Where(a => a.txtNomorID == paramData.txtNomorID && a.bitActive == true && a.bitSentMail == false).FirstOrDefault();
                        if (resultInvestigation != null)
                        {
                            foreach (var file in paramData.listDocument)
                            {
                                mAttachmentResult attachment = new mAttachmentResult();
                                string message = AttachmentValidation(file.listAttachment, context, attachment);

                                if (String.IsNullOrEmpty(message))
                                {
                                    InsertAttachment(attachment, file.listAttachment);
                                    
                                    attachment.txtNomorID = paramData.txtNomorID;
                                    attachment.txtFileDescription = file.FileDescription;
                                    attachment.bitActive = true;
                                    attachment.txtFileName = file.listAttachment.FileName;
                                    attachment.dtmInserted = DateTime.UtcNow;
                                    attachment.txtInsertedBy = "Manual"; //will be change to user login

                                    context.mAttachmentResult.Add(attachment);

                                    mHistoryNote history = new();
                                    history.txtNomorAduan = paramData.txtNomorID;
                                    history.Action = "Menambahkan lampiran " + file.listAttachment.FileName;
                                    history.txtNote = paramData.Notes;
                                    history.dtmInserted = attachment.dtmInserted;
                                    history.txtInsertedBy = attachment.txtInsertedBy;
                                    history.txtNote = paramData.Notes;

                                    context.mHistoryNote.Add(history);                                    
                                }
                                else
                                {
                                    return ResponseHandler.SendResponse(message);
                                }
                            }

                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            return ResponseHandler.SendResponse("Mohon diperiksa kembali data yang Anda lampirkan.");
                        }

                        return ResponseHandler.SendResponse("Lampiran berhasil di submit");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region DeleteAttachment
        public string DeleteAttachment(int id)
        {
            using (var context = new WBSDBContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var fullPath = Path.GetFullPath(ClsGlobalClass.GetRootPath + ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest);
                        var extAttachment = context.mAttachmentResult.Where(x => x.intAttachmentID == id && x.bitActive == true).FirstOrDefault();
                        var extResult = context.mResultInvestigation.Where(x => x.txtNomorID == extAttachment.txtNomorID && x.bitActive == true).FirstOrDefault();
                        clsCommonFunction.checkAndDeleteAttacment(fullPath, extAttachment.txtEncryptedName);

                        extAttachment.bitActive = false;
                        extAttachment.dtmUpdated = DateTime.UtcNow;
                        extAttachment.txtInsertedBy = "Manual"; //will be change to user login

                        context.mAttachmentResult.Update(extAttachment);

                        mHistoryNote history = new();
                        history.txtNomorAduan = extResult.txtNomorID;
                        history.Action = "Menghapus lampiran " + extAttachment.txtFileName;
                        history.dtmInserted = extAttachment.dtmInserted;
                        history.txtInsertedBy = extAttachment.txtInsertedBy;

                        context.mHistoryNote.Add(history);
                        context.SaveChanges();
                        transaction.Commit();

                        return ResponseHandler.SendResponse("Lampiran berhasil di hapus");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.Message);
                        throw;
                    }
                }                
            }            
        }
        #endregion

        #region AttachmentValidation
        public string AttachmentValidation(IFormFile file, WBSDBContext context, mAttachmentResult attachment)
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
        #endregion

        #region AllowedExtensionAttachment
        public bool AllowedExtensionAttachment(WBSDBContext context, IFormFile file, mAttachmentResult attachment)
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
        #endregion

        #region MaxSizeAttachment
        public bool MaxSizeAttachment(WBSDBContext context, IFormFile file, mAttachmentResult attachment)
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
        #endregion

        #region InsertAttachment
        public void InsertAttachment(mAttachmentResult attachment, IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var fileExt = fi.Extension;
            var fileName = Guid.NewGuid().ToString() + fileExt;
            attachment.txtEncryptedName = clsCommonFunction.saveAttachment(file, ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest, fileName);
            var pathAttachment = ClsGlobalConstant.PathAttachmentConstant.pathAttachmentTest;
            pathAttachment = pathAttachment.Replace("~/", "").Replace("/", Path.DirectorySeparatorChar.ToString());
            attachment.txtFilePath = ClsGlobalClass.GetRootPath + pathAttachment + fileName;
        }
        #endregion

        #region SendEmail
        public string SendEmail(WBSDBContext context, string nomor, List<string> listEmail, List<string> listUserName, List<string> listRoleName)
        {
            string rn = "";
            string un = "";

            string subjectMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailSubject" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            string fromMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailFrom" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            string bodyMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailBody" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            var ccMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailCC" && c.bitActive == true)
                                            .Select(c => c.txtValue).ToList();

            bodyMailInvestigation = bodyMailInvestigation.Replace("##NOMORADUAN##", nomor);

            foreach (var item in listUserName)
            {
                un += "<td>" + item + "</td>";
            }

            foreach (var item in listRoleName)
            {
                rn += "<td>" + item + "</td>";
            }

            bodyMailInvestigation = bodyMailInvestigation.Replace("##NAME##", un);
            bodyMailInvestigation = bodyMailInvestigation.Replace("##ROLE##", rn);

            cstmMailModel mailModel = new cstmMailModel();
            mailModel.txtSubject = subjectMailInvestigation;
            mailModel.txtBody = fromMailInvestigation;
            mailModel.txtFrom = bodyMailInvestigation;
            mailModel.listTxtCC = ccMailInvestigation;

            foreach (var item in listEmail)
            {
                mailModel.txtTo += item + ";";
            }

            try
            {
                clsCommonFunction.SendEmailViaKNGlobal(mailModel);
            }
            catch (Exception ex)
            {
                return ResponseHandler.SendResponse(ex.Message);
            }

            return ResponseHandler.SendResponse("Data berhasil di submit");
        }
        #endregion

        #region GetFormatNote
        public string GetFormatNote(string type, string extNote)
        {
            string notes = "";
            if (type == "Revise")
            {
                var dateNow = DateTime.Now.ToString("dd MMM yyyy hh:mm:ss");
                notes = dateNow + "-" + type + "-" + extNote;
            }

            return notes;
        }
        #endregion
    }
}
