using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public class mUser
    {
        public mUser()
        {
            ltRoles = new List<mRole>();
            ltActiveRoles = new List<mRole>();
            activeRole = new mRole();
        }

        public int intUserID { get; set; }
        public string txtUserName { get; set; }
        public string txtFullName { get; set; }
        public string txtNick { get; set; }
        public string txtEmployeeID { get; set; }
        public string txtEmail { get; set; }
        public string txtDomainUser { get; set; }
        public string txtPassword { get; set; }
        public Nullable<int> intOraclePersonID { get; set; }
        public Nullable<bool> bitUseAD { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public Nullable<System.DateTime> dtmLastLogin { get; set; }
        public string txtOrg_Group_Code { get; set; }
        public string txtOrganizationCode { get; set; }
        public string txtNaturalAccount { get; set; }
        public string txtCostCenter { get; set; }
        public string txtCabang { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public string txtGUID { get; set; }
        public Nullable<bool> bitDeleted { get; set; }
        public Nullable<System.DateTime> dtDeleted { get; set; }
        public List<mRole> ltRoles { get; set; }

        //ADDITIONAL
        public string txtOrg_Group_Name { get; set; }
        public mRole activeRole { get; set; }
        public string txtWso_Token { get; set; }
        public string txtBE_Token { get; set; }
        public string txtExpired { get; set; } // FOR TOKEN
        public DateTime dtmExpired { get; set; }// FOR TOKEN
        public List<mRole> ltActiveRoles { get; set; } // FOR SSO
    }
}
