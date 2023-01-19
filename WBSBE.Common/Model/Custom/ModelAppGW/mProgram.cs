using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public class mProgram
    {
        public int intProgramID { get; set; }
        public string txtProgramCode { get; set; }
        public string txtProgramName { get; set; }
        public string txtSolutionName { get; set; }
        public string txtSourceRepository { get; set; }
        public string txtDescription { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public Nullable<int> intDevelopmentYear { get; set; }
        public Nullable<int> intGoLiveYear { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public string txtGUID { get; set; }
        public Nullable<bool> bitDeleted { get; set; }
        public Nullable<System.DateTime> dtDeleted { get; set; }
        public string txtSecurity_Username { get; set; }
        public string txtSecurity_Password { get; set; }
        public string txtSecurity_Type { get; set; }
        public string txtSecurity_UrlToken { get; set; }
        public string txtSecurity_ClientID { get; set; }
    }
}
