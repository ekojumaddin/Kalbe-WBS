using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public class mMenu
    {
        public int intMenuID { get; set; }
        public int intProgramID { get; set; }
        public int intParentID { get; set; }
        public int intModuleID { get; set; }
        public string txtMenuCode { get; set; }
        public string txtMenuName { get; set; }
        public string txtPrefix { get; set; }
        public string txtDescription { get; set; }
        public string txtLink { get; set; }
        public Nullable<int> intOrder { get; set; }
        public Nullable<bool> bitActive { get; set; }
        public string txtGUID { get; set; }

        // Custom
        public List<mMenu> itemList { get; set; }

        public static mMenu CreateBlankmMenu()
        {
            mMenu dat = new mMenu();
            dat.intMenuID = 0;
            dat.txtMenuName = string.Empty;
            dat.bitActive = false;
            dat.intModuleID = 0;
            dat.intOrder = 0;
            dat.intParentID = 0;
            dat.txtDescription = string.Empty;
            dat.txtLink = string.Empty;
            dat.txtMenuName = string.Empty;
            dat.txtGUID = string.Empty;
            dat.intProgramID = 0;
            dat.txtMenuCode = string.Empty;
            dat.txtPrefix = string.Empty;

            return dat;
        }
    }
}
