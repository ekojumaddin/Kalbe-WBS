using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Library.Interface
{
    public interface INewOperation<T, DBContext>
    {
        List<T> GetAllData(DBContext context);
        List<T> GetAllData();
        T GetDataById(int paramId, DBContext context);
        T GetDataById(string paramTxtId);
        ClsPagingModelResponse GetAllDataDataTable(ClsPagingModelRequest paramData);
        ClsPagingModelResponse GetAllDataActiveTable(ClsPagingModelRequest paramData);
        string Update(T paramData);
        string Update(T paramData, DBContext context);
        void Delete(string paramTxtId);
        void Delete(string paramTxtId, DBContext context);
        void Insert(T paramData);
        void Insert(T paramData, DBContext context);
    }
}