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

namespace WBSBE.BussLogic
{
    public class clsMLookup : IOperation<mLookup, WBSDBContext>
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

        public List<mLookup> GetAllData(WBSDBContext context)
        {
            try
            {
                var result = new List<mLookup>();
                result = context.mLookup.Where(l => l.bitActive == true).OrderBy(l => l.txtType).ThenBy(l => l.intValue).ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<mLookup> GetAllData()
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

        public mLookup GetDataById(int paramId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public mLookup GetDataById(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Insert(mLookup paramData)
        {
            throw new NotImplementedException();
        }

        public string Insert(mLookup paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public string Update(mLookup paramData)
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

        public string Update(mLookup paramData, WBSDBContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var existLookup = context.mLookup.Where(x => x.intLookupID == paramData.intLookupID && x.bitActive == true).FirstOrDefault();
                    existLookup.txtType = paramData.txtType;
                    existLookup.txtName = paramData.txtName;
                    existLookup.intValue = paramData.intValue;
                    existLookup.intOrderNo = paramData.intOrderNo;
                    existLookup.bitActive = paramData.bitActive;
                    existLookup.dtmUpdated = DateTime.UtcNow;
                    existLookup.txtUpdatedBy = "Manual"; //will be change to user login
                    context.Update(existLookup);
                    context.SaveChanges();
                    transaction.Commit();

                    return ResponseHandler.SendResponse("Data berhasil diubah");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
