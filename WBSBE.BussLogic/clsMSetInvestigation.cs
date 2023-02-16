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
    public class clsMSetInvestigation : IOperation<SetTeamInvestigationModel, WBSDBContext>
    {
        #region Property 
        LoggerManager logger = new LoggerManager();
        #endregion

        public List<mUser> GetAllUser()
        {
            using (var context = new WBSDBContext())
            {
                List<mUser> listUser = new List<mUser>();

                var query = context.mUser.Where(p => p.bitActive == true).ToList();

                foreach (var item in query)
                {
                    mUser model = new mUser();
                    model.intUserID = item.intUserID;
                    model.txtUserName = item.txtUserName;

                    listUser.Add(model);
                }

                return listUser;
            }            
        }

        public List<mRole> GetAllRole()
        {
            using (var context = new WBSDBContext())
            {
                List<mRole> listRole = new List<mRole>();

                var query = context.mRole.Where(p => p.bitActive == true).ToList();

                foreach (var item in query)
                {
                    mRole model = new mRole();
                    model.intRoleID = item.intRoleID;
                    model.txtRoleName = item.txtRoleName;
                    listRole.Add(model);
                }

                return listRole;
            }
        }

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

                    if (dt.bitActive != false)
                    {

                        dt.listJawaban = context.mJawaban.Where(x => x.txtNomorAduan.txtNomorID == paramTxtId).ToList();
                        dt.listAttachments = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramTxtId).ToList();
                        dt.bitActive = false;
                        dt.dtmUpdated = DateTime.UtcNow;
                        dt.txtUpdatedBy = "Manual";

                        foreach (var dtJawaban in dt.listJawaban)
                        {
                            dtJawaban.bitActive = false;
                            dtJawaban.dtmUpdated = DateTime.UtcNow;
                            dtJawaban.txtUpdatedBy = "Manual";
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

                    else
                    {
                        dt.listJawaban = context.mJawaban.Where(x => x.txtNomorAduan.txtNomorID == paramTxtId).ToList();
                        dt.listAttachments = context.mAttachment.Where(x => x.mAduan.txtNomorID == paramTxtId).ToList();
                        dt.bitActive = true;
                        dt.dtmUpdated = DateTime.UtcNow;
                        dt.txtUpdatedBy = "Manual";

                        foreach (var dtJawaban in dt.listJawaban)
                        {
                            dtJawaban.bitActive = true;
                            dtJawaban.dtmUpdated = DateTime.UtcNow;
                            dtJawaban.txtUpdatedBy = "Manual";
                        }

                        foreach (var dtAttachment in dt.listAttachments)
                        {
                            dtAttachment.bitActive = true;
                            dtAttachment.dtmUpdated = DateTime.UtcNow;
                            dtAttachment.txtUpdatedBy = "Manual";
                        }

                        context.Update(dt);
                        context.SaveChanges();
                        transaction.Commit();
                        return paramTxtId;
                    }


                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<SetTeamInvestigationModel> GetAllData(WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<SetTeamInvestigationModel> GetAllData()
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

        public SetTeamInvestigationModel GetDataById(int paramId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public SetTeamInvestigationModel GetDataById(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public SetTeamInvestigationModel GetAllAduanById(int paramId)
        {
            using (var context = new WBSDBContext())
            {
                var nomor = context.mSetInvestigation.Where(a => a.intUserID == paramId && a.bitActive == true).Select(a => a.txtNomorID).ToList();

                SetTeamInvestigationModel investigationModel = new SetTeamInvestigationModel();
                investigationModel.listAduan = new();

                if (nomor != null)
                {
                    foreach (var item in nomor)
                    {
                        AduanModel aduan = new AduanModel();
                        aduan.listTanyaJawab = new();
                        aduan.fileName = new();

                        var getNomor = context.mAduan.Where(a => a.txtNomorID == item && a.bitActive == true).FirstOrDefault();                     

                        aduan.txtNomorID = getNomor.txtNomorID;
                        aduan.txtStatus = getNomor.txtStatus;
                        aduan.txtPelapor = getNomor.txtPelapor;
                        aduan.txtNIK = getNomor.txtNIK;
                        aduan.txtNama = getNomor.txtNama;
                        aduan.txtTlp = getNomor.txtTlp;
                        aduan.txtEmail = getNomor.txtEmail;

                        investigationModel.listAduan.Add(aduan);

                        var listTanyaJawab = (from j in context.mJawaban
                                              join p in context.mPertanyaan on j.intPertanyaanID equals p.intPertanyaanID
                                              where j.bitActive == true && p.bitActive == true && j.txtNomorAduan.txtNomorID == item
                                              orderby p.intOrderPertanyaan
                                              select new
                                              {
                                                  jwbId = j.intJawabanID,
                                                  ptyId = p.intPertanyaanID,
                                                  orderId = p.intOrderPertanyaan,
                                                  pertanyaan = p.txtPertanyaan,
                                                  jawaban = j.txtJawaban,
                                                  mandatory = p.bitMandatory
                                              }).ToList();

                        if (listTanyaJawab.Count > 0)
                        {
                            foreach (var itemTJ in listTanyaJawab)
                            {
                                TanyaJawabModel tanyaJawab = new TanyaJawabModel();
                                tanyaJawab.txtPertanyaan = itemTJ.pertanyaan;
                                tanyaJawab.txtJawaban = itemTJ.jawaban;

                                aduan.listTanyaJawab.Add(tanyaJawab);
                            }
                        }

                        var listLampiran = context.mAttachment.Where(c => c.mAduan.txtNomorID == item && c.bitActive == true).ToList();

                        if (listLampiran.Count > 0)
                        {
                            foreach (var lampiran in listLampiran)
                            {
                                var oriFileName = lampiran.txtFileName;

                                aduan.fileName.Add(oriFileName);
                            }
                        }
                    }
                }

                return investigationModel;
            }
        }

        public string Insert(SetTeamInvestigationModel paramData)
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

        public string Insert(SetTeamInvestigationModel paramData, WBSDBContext context)
        {
            foreach (var item in paramData.listTeamInvestigation)
            {
                mSetInvestigation team = new mSetInvestigation();
                if (item.intUserID.HasValue && item.intRoleID.HasValue)
                {                    
                    team.intUserID = (int)item.intUserID;
                    team.intRoleID = (int)item.intRoleID;
                }
                else 
                {
                    ResponseHandler.SendResponse("Nama Team dan Role wajib diisi");
                }

                team.txtNomorID = paramData.txtNomorID;
                team.bitActive = true;
                team.bitSubmit = false;
                team.bitSentMail = false;
                team.dtInserted = DateTime.UtcNow;
                team.txtInsertedBy = "Manual";

                context.mSetInvestigation.Add(team);
                context.SaveChanges();
            }

            return ResponseHandler.SendResponse("Data berhasil di simpan");
        }

        public string Submit(SetTeamInvestigationModel paramData)
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

        public string Submit(SetTeamInvestigationModel paramData, WBSDBContext context)
        {
            List<string> listEmail = new List<string>();
            List<string> listUserName = new List<string>();
            List<string> listRoleName = new List<string>();
            List<mSetInvestigation> listIdInvestigation = new List<mSetInvestigation>();
            string rn = "";
            string un = "";

            try
            {
                foreach (var item in paramData.listTeamInvestigation)
                {
                    var email = context.mUser.Where(u => u.intUserID == item.intUserID).Select(u => u.txtEmail).FirstOrDefault();
                    if (email != null)
                    {
                        listEmail.Add(email);
                    }

                    var username = context.mUser.Where(u => u.intUserID == item.intUserID).Select(u => u.txtUserName).FirstOrDefault();
                    if (username != null)
                    {
                        listUserName.Add(username);
                    }

                    var rolename = context.mRole.Where(u => u.intRoleID == item.intRoleID).Select(u => u.txtRoleName).FirstOrDefault();
                    if (rolename != null)
                    {
                        listRoleName.Add(rolename);
                    }

                    mSetInvestigation team = new mSetInvestigation();
                    if (item.intUserID.HasValue && item.intRoleID.HasValue)
                    {
                        team.intUserID = (int)item.intUserID;
                        team.intRoleID = (int)item.intRoleID;
                    }
                    else
                    {
                        ResponseHandler.SendResponse("Nama Team dan Role wajib diisi");
                    }

                    team.txtNomorID = paramData.txtNomorID;
                    team.bitActive = true;
                    team.bitSubmit = true;
                    team.bitSentMail = false;
                    team.dtInserted = DateTime.UtcNow;
                    team.txtInsertedBy = "Manual";

                    context.mSetInvestigation.Add(team);
                    context.SaveChanges();

                    listIdInvestigation.Add(team);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace.ToString());
            }


            #region sendingEmail  
            string subjectMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailSubject" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            string fromMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailFrom" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            string bodyMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailBody" && c.bitActive == true)
                                            .Select(c => c.txtValue).FirstOrDefault();
            var ccMailInvestigation = context.mConfig.Where(c => c.txtType == "SetTeamInvestigation" && c.txtName == "MailCC" && c.bitActive == true)
                                            .Select(c => c.txtValue).ToList();

            bodyMailInvestigation = bodyMailInvestigation.Replace("##NOMORADUAN##", paramData.txtNomorID);

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

                foreach (var item in listIdInvestigation)
                {
                    item.bitSentMail = true;
                    context.Update(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return ResponseHandler.SendResponse(ex.Message);
            }
            #endregion

            return ResponseHandler.SendResponse("Data berhasil di simpan");
        }

        public string Update(SetTeamInvestigationModel paramData)
        {
            throw new NotImplementedException();
        }

        public string Update(SetTeamInvestigationModel paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
