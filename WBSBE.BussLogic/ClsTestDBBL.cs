using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Entity.WBS;
using WBSBE.Common.Library;
using WBSBE.Common.Library.Interface;
using WBSBE.DAL.Context;

namespace WBSBE.BussLogic
{
    public class ClsTestDBBL : IOperation<TestDb, WBSDBContext>
    {
        public string Delete(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Delete(string paramTxtId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public List<TestDb> GetAllData(WBSDBContext context)
        {
            return context.TestDbs.ToList();
        }

        public List<TestDb> GetAllData()
        {
            using var context = new WBSDBContext();
            try
            {
                return GetAllData(context);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
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

        public TestDb GetDataById(int paramId, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public TestDb GetDataById(string paramTxtId)
        {
            throw new NotImplementedException();
        }

        public string Insert(TestDb paramData)
        {
            throw new NotImplementedException();
        }

        public string Insert(TestDb paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }

        public string Update(TestDb paramData)
        {
            throw new NotImplementedException();
        }

        public string Update(TestDb paramData, WBSDBContext context)
        {
            throw new NotImplementedException();
        }
    }
}
