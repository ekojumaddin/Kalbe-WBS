using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class ListTeamInvestigationModel
    {
        public Nullable<int> intInvestigationID { get; set; }
        public Nullable<int> intUserID { get; set; }
        public Nullable<int> intRoleID { get; set; }
        public string? txtUserName { get; set; }
        public string? txtRoleName { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string? txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string? txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
    }
}
