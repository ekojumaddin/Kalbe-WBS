﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;
using WBSBE.Common.Library;
using WBSBE.Common.Library.Interface;
using WBSBE.Common.Model;
using WBSBE.DAL.Context;

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

                if (item.bitMandatory == true)
                {
                    model.isMandatory = "Mandatory";
                }
                else
                {
                    model.isMandatory = "Optional";
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
            foreach (var item in paramData.listPertanyaan)
            {
                if (item.intPertanyaanID > 0)
                {
                    var existPertanyaan = context.mPertanyaan.Where(p => p.intPertanyaanID == item.intPertanyaanID && p.bitActive == true).FirstOrDefault();
                    existPertanyaan.txtPertanyaan = item.txtPertanyaan;
                    existPertanyaan.intOrderPertanyaan = item.intOrderPertanyaan;
                    if (item.isActive == "Active")
                    {
                        existPertanyaan.bitActive = true;
                    }
                    else
                    {
                        existPertanyaan.bitActive = false;
                    }

                    if (item.bitMandatory.HasValue)
                    {
                        existPertanyaan.bitMandatory = (bool)item.bitMandatory;
                    }
                    existPertanyaan.dtmUpdated = DateTime.UtcNow;
                    existPertanyaan.txtUpdatedBy = "Manual"; //will be change to user login

                    context.Update(existPertanyaan);
                    context.SaveChanges();
                }
                else
                {
                    var checkExistNama = context.mPertanyaan.Where(p => p.txtPertanyaan == item.txtPertanyaan && p.bitActive == true).FirstOrDefault();
                    if (checkExistNama != null)
                    {
                        return ResponseHandler.SendResponse("Nama pertanyaan sudah tersedia");
                    }

                    mPertanyaan pertanyaan = new mPertanyaan();
                    pertanyaan.txtPertanyaan = item.txtPertanyaan;
                    pertanyaan.bitActive = true;
                    if (item.bitMandatory.HasValue)
                    {
                        pertanyaan.bitMandatory = (bool)item.bitMandatory;
                    }
                    pertanyaan.intOrderPertanyaan = item.intOrderPertanyaan;
                    pertanyaan.dtmInserted = DateTime.UtcNow;
                    pertanyaan.txtInsertedBy = "Manual";

                    context.Add(pertanyaan);
                    context.SaveChanges();
                }
            }

            var listPertanyaan = context.mPertanyaan.Where(p => p.bitActive == true).Select(p => p.intPertanyaanID).ToList();
            foreach (var item in listPertanyaan)
            {
                if (!paramData.listPertanyaan.Exists(x => x.intPertanyaanID == item))
                {
                    var nonActivePertanyaan = context.mPertanyaan.Where(p => p.intPertanyaanID == item && p.bitActive == true).FirstOrDefault();
                    nonActivePertanyaan.bitActive = false;

                    context.Update(nonActivePertanyaan);
                    context.SaveChanges();
                }
            }

            return ResponseHandler.SendResponse("Data berhasil di simpan");
        }

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
                    if (paramData.bitMandatory.HasValue)
                    {
                        existPertanyaan.bitMandatory = (bool)paramData.bitMandatory;
                    }
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
