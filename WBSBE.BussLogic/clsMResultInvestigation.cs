using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

                var existsAttachment = context.mAttachmentResult.Where(a => a.mResultInvestigation.intResultInvestigationID == existsNomor.intResultInvestigationID).ToList();

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
            throw new NotImplementedException();
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
            try
            {
                mResultInvestigation result = new mResultInvestigation();
                result.txtNomorID = paramData.txtNomorID;
                result.txtExecutive = paramData.ExecutiveSummary;
                result.txtNote = paramData.Notes;
                result.bitSubmit = false;
                result.bitSentMail = false;
                result.dtInserted = DateTime.UtcNow;
                result.txtInsertedBy = "Manual";
                result.bitActive = true;

                foreach (var file in paramData.listDocument)
                {
                    mAttachmentResult attachment = new ();
                    string message = AttachmentValidation(file.listAttachment, context, attachment);

                    if (String.IsNullOrEmpty(message))
                    {
                        InsertAttachment(attachment, file.listAttachment);
                        attachment.txtFileDescription = file.FileDescription;
                        attachment.bitActive = true;
                        attachment.txtFileName = file.listAttachment.FileName;
                        attachment.dtmInserted = DateTime.UtcNow;
                        attachment.txtInsertedBy = "Manual"; //will be change to user login                                    

                        result.listAttachments.Add(attachment);
                    }
                    else
                    {
                        return ResponseHandler.SendResponse(message);
                    }
                }

                context.mResultInvestigation.Add(result);
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
                //    clsCommonFunction.SendEmailViaKNGlobal(mailModelComplainer);
                //    clsCommonFunction.SendEmailViaKNGlobal(mailModelCommitee);

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
                        existingResultInvestigations.bitSubmit = true;
                        existingResultInvestigations.dtUpdated = DateTime.UtcNow;
                        existingResultInvestigations.txtUpdatedBy = "Manual";

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
                        result.txtInsertedBy = "Manual";
                        result.bitActive = true;

                        foreach (var file in paramData.listDocument)
                        {
                            mAttachmentResult attachment = new();
                            string message = AttachmentValidation(file.listAttachment, context, attachment);

                            if (String.IsNullOrEmpty(message))
                            {
                                InsertAttachment(attachment, file.listAttachment);
                                attachment.txtFileDescription = file.FileDescription;
                                attachment.bitActive = true;
                                attachment.txtFileName = file.listAttachment.FileName;
                                attachment.dtmInserted = DateTime.UtcNow;
                                attachment.txtInsertedBy = "Manual"; //will be change to user login

                                result.listAttachments.Add(attachment);
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

                //foreach (var item in listIdInvestigation)
                //{
                //    item.bitSentMail = true;
                //    context.Update(item);
                //    context.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                return ResponseHandler.SendResponse(ex.Message);
            }

            return ResponseHandler.SendResponse("Data berhasil di submit");
        }
    }
}
