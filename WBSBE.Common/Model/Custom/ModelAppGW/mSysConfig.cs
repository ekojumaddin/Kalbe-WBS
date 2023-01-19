using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public class mSysConfig
    {
        public int intSysConfigID { get; set; }
        public int intProgramID { get; set; }
        public string txtModuleID { get; set; }
        public string txtKeyId { get; set; }
        public string txtDescription { get; set; }
        public string txtValueId { get; set; }
        public string txtDefaultValueId { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public string txtGUID { get; set; }
        public Nullable<bool> bitDeleted { get; set; }
        public Nullable<System.DateTime> dtDeleted { get; set; }
        public Nullable<int> intCompanyID { get; set; }
        public int intIndex { get; set; }

        public mSysConfig() { }

        public mSysConfig(string _txtModuleID, string _txtKeyId, string _txtDescription, string _txtDefaultValueId, int _intProgramID)
        {
            this.txtModuleID = _txtModuleID;
            this.txtKeyId = _txtKeyId;
            this.txtDescription = _txtDescription;
            this.txtDefaultValueId = _txtDefaultValueId;
            this.txtValueId = string.Empty;
            this.intProgramID = _intProgramID;
        }
    }
}
