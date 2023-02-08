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
using WBSBE.DAL.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WBSBE.BussLogic
{
    public class clsMPertanyaan : IOperation<PertanyaanModel, WBSDBContext>
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

        public string DeleteData(int? paramTxtId)
        {
            try
            {
                using (var context = new WBSDBContext())
                {
                    return DeleteData(paramTxtId, context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public string DeleteData(int? paramTxtId, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var dt = context.mPertanyaan.Where(x => x.intPertanyaanID == paramTxtId).FirstOrDefault();

                    if (dt.bitActive != false)
                    {
                        dt.bitActive = false;
                        dt.dtmUpdated = DateTime.UtcNow;
                        dt.txtUpdatedBy = "Manual";

                        context.Update(dt);
                        context.SaveChanges();
                        transaction.Commit();
                        return ResponseHandler.SendResponse("Data Pertanyaan untuk Nomor Urut" + dt.intOrderPertanyaan + "telah dinonaktifkan");
                    }

                    else
                    {
                        dt.bitActive = true;
                        dt.dtmUpdated = DateTime.UtcNow;
                        dt.txtUpdatedBy = "Manual";

                        context.Update(dt);
                        context.SaveChanges();
                        transaction.Commit();
                        return ResponseHandler.SendResponse("Data Pertanyaan untuk Nomor Urut" + dt.intOrderPertanyaan + "telah diaktifkan");
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

        public List<PertanyaanModel> GetAllData(WBSDBContext context)
        {
            List<PertanyaanModel> listPertanyaan = new List<PertanyaanModel>();

            var query = context.mPertanyaan.Where(p => p.bitActive == true).ToList();

            foreach (var item in query)
            {
                PertanyaanModel model = new PertanyaanModel();
                model.intPertanyaanID = item.intPertanyaanID;
                model.txtPertanyaan = item.txtPertanyaan;
                model.intOrderPertanyaan = item.intOrderPertanyaan;
                model.bitMandatory =  item.bitMandatory;
                model.bitActive = item.bitActive;

                if (item.bitMandatory == true)
                {
                    model.isMandatory = "(*)";
                }
                else
                {
                    model.isMandatory = "";
                }

                if (item.bitActive == true)
                {
                    model.isActive = "Active";
                }
                else
                {
                    model.isActive = "Inactive";
                }

                listPertanyaan.Add(model);
            }

            return listPertanyaan;
            
        }

        public List<PertanyaanModel> GetAllData()
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

        public PertanyaanModel GetDataById(int paramId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public PertanyaanModel GetDataById(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Insert(PertanyaanModel paramData)
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

        public string Insert(PertanyaanModel paramData, WBSDBContext context)
        {
            var checkExistOrder = context.mPertanyaan.Where(p => p.intOrderPertanyaan == paramData.intOrderPertanyaan && p.bitActive == true).FirstOrDefault();
            if (checkExistOrder != null)
            {
                return ResponseHandler.SendResponse("Nomor urutan pertanyaan sudah tersedia");
            }

            var checkExistNama = context.mPertanyaan.Where(p => p.txtPertanyaan == paramData.txtPertanyaan && p.bitActive == true).FirstOrDefault();
            if (checkExistNama != null)
            {
                return ResponseHandler.SendResponse("Nama pertanyaan sudah tersedia");
            }

            mPertanyaan pertanyaan = new mPertanyaan();
            pertanyaan.txtPertanyaan = paramData.txtPertanyaan;
            pertanyaan.intOrderPertanyaan = paramData.intOrderPertanyaan;
            pertanyaan.bitMandatory = paramData.bitMandatory;
            pertanyaan.bitActive = true;
            pertanyaan.dtmInserted = DateTime.UtcNow;
            pertanyaan.txtInsertedBy = "Manual";

            context.mPertanyaan.Add(pertanyaan);
            context.SaveChanges();

            return ResponseHandler.SendResponse("Data berhasil di simpan");
        }

        //public string Change(PertanyaanModel paramData, WBSDBContext context)
        //{
        //    try
        //    {
        //        mPertanyaan pertanyaan = new mPertanyaan();
        //        pertanyaan.txtPertanyaan = paramData.txtPertanyaan;
        //        pertanyaan.intOrderPertanyaan = paramData.intOrderPertanyaan;
        //        pertanyaan.bitMandatory = paramData.bitMandatory;
        //        pertanyaan.bitActive = true;
        //        pertanyaan.dtmInserted = DateTime.UtcNow;
        //        pertanyaan.txtInsertedBy = "Manual";

        //        var getDataDB = per

        //        foreach (var item in paramData.listJawaban)
        //        {
        //            mJawaban jawaban = new mJawaban();
        //            int j = (int)item.intPertanyaanID;
        //            for (int i = 0; i <= j; i++)
        //            {
        //                jawaban.intOrderJawaban = i;
        //            }
        //            jawaban.txtJawaban = item.txtJawaban;
        //            jawaban.bitActive = true;
        //            jawaban.dtmInserted = DateTime.UtcNow;
        //            jawaban.txtInsertedBy = "Manual";

        //            if (item.intPertanyaanID.HasValue)
        //            {
        //                int idPertanyaan = j;
        //                jawaban.intPertanyaanID = idPertanyaan;
        //            }

        //            aduan.listJawaban.Add(jawaban);
        //        }

        //        foreach (IFormFile file in paramData.fileData)
        //        {
        //            mAttachment attachment = new mAttachment();
        //            string message = AttachmentValidation(file, context, attachment);

        //            if (String.IsNullOrEmpty(message))
        //            {
        //                InsertAttachment(attachment, file);

        //                attachment.bitActive = true;
        //                attachment.txtFileName = file.FileName;
        //                attachment.dtmInserted = DateTime.UtcNow;
        //                attachment.txtInsertedBy = "Manual"; //will be change to user login                                    

        //                aduan.listAttachments.Add(attachment);
        //            }
        //            else
        //            {
        //                return ResponseHandler.SendResponse(message);
        //            }
        //        }

        //        context.mAduan.Add(aduan);
        //        context.SaveChanges();

        //        return ResponseHandler.SendResponse("Data berhasil di simpan");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResponseHandler.SendResponse(ex.Message);
        //    }


        //}

        public string Update(PertanyaanModel paramData)
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

        public string Update(PertanyaanModel paramData, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existPertanyaan = context.mPertanyaan.Where(p => p.intPertanyaanID == paramData.intPertanyaanID && p.bitActive == true).FirstOrDefault();
                    existPertanyaan.txtPertanyaan = paramData.txtPertanyaan;
                    existPertanyaan.intOrderPertanyaan = paramData.intOrderPertanyaan;
                    existPertanyaan.bitMandatory = paramData.bitMandatory;
                    existPertanyaan.dtmUpdated = DateTime.UtcNow;
                    existPertanyaan.txtUpdatedBy = "Manual"; //will be change to user login

                    context.Update(existPertanyaan);
                    context.SaveChanges();
                    transaction.Commit();

                    return ResponseHandler.SendResponse("Data berhasil diubah");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
