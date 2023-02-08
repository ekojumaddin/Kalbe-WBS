using System;
using WBSBE.Common.Library;
using Newtonsoft.Json.Linq;

namespace WBSBE.Common.Model.Custom.ModelAppGW
{
    public partial class trDebugEmail
    {
        public int intDebugEmailID { get; set; }
        public Nullable<int> intProgramID { get; set; }
        public string txtFrom { get; set; }
        public string txtTo { get; set; }
        public string txtCC { get; set; }
        public string txtBCC { get; set; }
        public string txtSubject { get; set; }
        public string txtPriority { get; set; }
        public Nullable<bool> bitIsBodyHTML { get; set; }
        public string txtBody { get; set; }
        public string txtInsertedBy { get; set; }
        public Nullable<System.DateTime> dtInserted { get; set; }
        public string txtUpdatedBy { get; set; }
        public Nullable<System.DateTime> dtUpdated { get; set; }
        public string txtGUID { get; set; }

        public static trDebugEmail parseFromJSON(JObject jsonDat, bool bitPost, bool bitSave)
        {
            trDebugEmail dat = new trDebugEmail();
            dat.intDebugEmailID = ClsGlobalConstant.ParseToInteger(jsonDat["intDebugEmailID"].ToString());
            dat.intProgramID = ClsGlobalConstant.ParseToInteger(jsonDat["intProgramID"].ToString());
            dat.txtFrom = ClsGlobalConstant.ParseToString(jsonDat["txtFrom"].ToString());
            dat.txtTo = ClsGlobalConstant.ParseToString(jsonDat["txtTo"].ToString());
            dat.txtCC = ClsGlobalConstant.ParseToString(jsonDat["txtCC"].ToString());
            dat.txtBCC = ClsGlobalConstant.ParseToString(jsonDat["txtBCC"].ToString());
            dat.txtSubject = ClsGlobalConstant.ParseToString(jsonDat["txtSubject"].ToString());
            dat.txtPriority = ClsGlobalConstant.ParseToString(jsonDat["txtPriority"].ToString());
            dat.bitIsBodyHTML = ClsGlobalConstant.ParseToBoolean(jsonDat["bitIsBodyHTML"].ToString());
            dat.txtBody = ClsGlobalConstant.ParseToString(jsonDat["txtBody"].ToString());
            dat.txtGUID = ClsGlobalConstant.ParseToString(jsonDat["txtGUID"].ToString());

            dat.dtInserted = ClsGlobalConstant.ParseToDateTime(jsonDat["dtInserted"].ToString());
            dat.dtUpdated = ClsGlobalConstant.ParseToDateTime(jsonDat["dtUpdated"].ToString());
            dat.txtInsertedBy = ClsGlobalConstant.ParseToString(jsonDat["txtInsertedBy"].ToString());
            dat.txtUpdatedBy = ClsGlobalConstant.ParseToString(jsonDat["txtUpdatedBy"].ToString());
            dat.txtUpdatedBy = ClsGlobalConstant.ParseToString(jsonDat["txtUpdatedBy"].ToString());
            dat.txtGUID = ClsGlobalConstant.ParseToString(jsonDat["txtGUID"].ToString());

            if (bitPost)
            {
                // Date Time
                dat.dtInserted = DateTime.Parse(dat.dtInserted.ToString()).AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime()).Hours);
                dat.dtUpdated = DateTime.Parse(dat.dtUpdated.ToString()).AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime()).Hours);
            }

            return dat;
        }



        public static trDebugEmail CreateBlankClstrDebugEmail()
        {
            trDebugEmail dat = new trDebugEmail();
            dat.intDebugEmailID = 0;
            dat.intProgramID = 0;
            dat.txtFrom = string.Empty;
            dat.txtTo = string.Empty;
            dat.txtCC = string.Empty;
            dat.txtBCC = string.Empty;
            dat.txtSubject = string.Empty;
            dat.txtPriority = string.Empty;
            dat.bitIsBodyHTML = false;
            dat.txtBody = string.Empty;
            dat.txtGUID = string.Empty;


            dat.txtInsertedBy = "";
            dat.dtInserted = ClsGlobalConstant.DATE_MINVALUE;
            dat.txtUpdatedBy = "";
            dat.dtUpdated = ClsGlobalConstant.DATE_MINVALUE;
            dat.txtUpdatedBy = "";

            return dat;
        }

    }
}
