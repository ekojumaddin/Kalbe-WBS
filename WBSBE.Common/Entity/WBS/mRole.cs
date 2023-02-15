using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Entity.WBS
{
    public class mRole
    {
        [Key]
        public int intRoleID { get; set; }
        public string txtRoleCode { get; set; }
        public string txtRoleName { get; set; }
        public string txtDescription { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public Nullable<bool> bitDeleted { get; set; }
        public Nullable<System.DateTime> dtDeleted { get; set; }
    }
}
