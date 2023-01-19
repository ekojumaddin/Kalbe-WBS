using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public class mRole
    {
        public int intRoleID { get; set; }
        public int intProgramID { get; set; }
        public string txtRoleCode { get; set; }
        public string txtRoleName { get; set; }
        public string txtDescription { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public Nullable<bool> bitSuperuser { get; set; }
        public string txtGUID { get; set; }
        public Nullable<bool> bitDefault { get; set; }
        public Nullable<bool> bitDeleted { get; set; }
        public Nullable<System.DateTime> dtDeleted { get; set; }
        public Nullable<int> intCompanyID { get; set; }
        public virtual mProgram mProgram { get; set; }
    }
}
