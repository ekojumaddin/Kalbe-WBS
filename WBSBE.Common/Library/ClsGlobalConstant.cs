using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Library
{
    public class ClsGlobalConstant
    {
        //=CLASS INI DIGUNAKAN UNTUK MENDEKLARASI CONSTANT PROPERTY= 
        public const int intProgramID = 144;//dapat saat di daftarkan APP GW, setelah run method di startupController //Editable Based On Project

        public const string MODULE_NAME = "WBS";
        public const string MODULE_ID = "WBS";
        public const string Administrator = "Administrator";
        public const string MESSAGE = "MESSAGE";
        public const string TOKEN_API = "Sanghiang";
        public const string txtProgramCode = "WBS"; //di daftarkan dulu program code manual di web appgw baru run yg di startup controller //Editable Based On Project 
        public const string DefaultLangID = "IN";
        public const string FormatDate = "yyyy/MM/dd";
        public static DateTime DATE_MINVALUE = DateTime.Parse("1/1/2000");
        public static DateTime DATE_MAXVALUE = DateTime.Parse("1/1/4000");

        #region Declare Constant
        public class sessionBooking
        {
            public const int fullTime = 3;
            public const int session2 = 2;
            public const int session1 = 1;
        }

        public class reddisConst
        {
            public const string reddisMenu = "[REPLACE WITH GUID]_menu";

        }

        public class statusReservation
        {
            public const string UNAVAILABLE = "UNAVAILABLE";
            public const string AVAILABLE = "AVAILABLE";
            public const string RESERVED = "RESERVED";
            public const string CHECK_IN = "CHECK IN";
            public const string CHECK_OUT = "CHECK OUT";
            public const string CANCELED = "CANCELED";
            public const string CANCELED_AUTO_RELEASE = "CANCELED [AUTO RELEASE]";
        }
        public class typeReport
        {
            public const string REPORT_PLAN_VS_ACTUAL_SUMMARY = "REPORT PLAN VS ACTUAL (SUMMARY)";
            public const string REPORT_PLAN_VS_ACTUAL_DETAIL = "REPORT PLAN VS ACTUAL (DETAIL)";
            public const string REPORT_PLAN_VS_ACTUAL_DETAILXDAILY = "REPORT PLAN VS ACTUAL (DETAIL - DAILY)";
            public const string REPORT_OCCUPANCY = "REPORT OCCUPANCY";
        }

        public class ParamDescription
        {
            public const string typeReport = "TypeReport";
            public const string bookingTime = "BookingTime";
            public const string statusDesk = "StatusDesk";
            public const string filterStatusMonitoring = "FilterStatusMonitoring";
        }

        public class typeQueryFilter
        {
            public const string oracle = "ORACLE";
            public const string sql = "SQL";
            public const string postgresql = "POSTGRESQL";

        }

        public class PathAttachmentConstant
        {
            public const string pathAttachmentTest = "~/Data/test/";
            public const string pathImageArea = "~/Data/Area/";
        }
        public class execptionConstant
        {
            public const string getDataNotFound = "Data not found in database, Contact IT";
            public const string specialCharacterFound = "Terdapat special karakter dalam inputan, hanya boleh huruf,angka dan tanda baca _ .";
            public const string validationRunningNumber = "Something went  wrong while create running number, Contanct IT!";
            public const string conformCauseNotMatched = "Conform & Cause not matched";
        }

        public class pathAttachmentConstant
        {
            public const string pathAttachmentTest = "~/Data/test/";
            public const string pathExportExcel = "~/Data/ExportExcel/";
        }

        public class SSO_CONST
        {
            public const string SSO_USER = "SSO_SYSTEM";
            public const string SSO_DUMMY_PASSWORD = "CONCEPT";
            public const string DefaultLangID = "IN";
            public const string MAIN_PROGRAM_CODE = "MAIN";
            public const string MAIN_PROGRAM_ID = "100";
            public const string DEFAULT_USER = "User";

            public const string WSO_checkUserPassword = "t/kalbenutritionals.com/KNGlobal/v1/api/1/AccountAPI/Login";
            public const string WSO_logOutSSO = "t/kalbenutritionals.com/KNGlobal/v1/api/1/UserAPI/logOutSSO";
            public const string WSO_getDataCookies = "t/kalbenutritionals.com/KNGlobal/v1/api/1/UserAPI/getDataCookies";
            public const string WSO_getUserInfoKNGLOBAL_SSO = "t/kalbenutritionals.com/KNGlobal/v1/api/1/UserAPI/getUserInfoKNGLOBAL_SSO";
        }

        public class key
        {
            // Default
            public const string SHOW_STACKTRACE = "Show Stack Trace";
            public const string ExceptionPublisherEmailSender = "Exception Sender";
            public const string ExceptionPublisherEmailSubject = "Exception Subjet";
            public const string SenderEmail = "Sender Email";
            public const string JoinString = "JoinString";
            public const string SEND_ERROREMAIL = "Send Email Error";
            public const string DefaultLangID = "DefaultLangID";
            public const string bDebugEmail = "Debug Email";
            public const string SMTP = "SMTP";
            public const string Generate_Random_Parameter = "Generate Random Parameter";
            public const string byPassLogin = "byPassLogin";
            public const string bSendEmailNotif = "bSendEmailNotif";
            public const string NotifPublisherEmailSender = "Notification Sender";
            public const string NotifPublisherEmailSubject = "Notification Subject";
            public const string bDefaultEmailNotif = "DefaultEmailNotif";
            public const string bOverideEmailNotif = "Overide Email Notif";
            public const string CONTENT_APPROVAL = "CONTENT APPROVAL";
            public const string CONTENT_APPROVED = "CONTENT APPROVED";
            public const string CONTENT_REJECTED = "CONTENT REJECTED";
            public const string NotifPublisherEmailSubject_Approval = "Notification Subject Approval";
            public const string NotifPublisherEmailSubject_Approved = "Notification Subject Approved";
            public const string NotifPublisherEmailSubject_Rejected = "Notification Subject Rejected";

            // Custom 
        }

        public class defaultConfigurationValue
        {
            // Default
            public const string SHOW_STACKTRACE = "N";
            public const string ExceptionPublisherEmailSender = "no-reply@kalbenutritionals.com";
            public const string ExceptionPublisherEmailSubject = "DeskBooking:ERROR";

            //DEV
            //public const string SenderEmail = "rizki.pamungkas@kalbenutritionals.com"; //Editable Based On Project
            //PROD
            public const string SenderEmail = "rizki.pamungkas@kalbenutritionals.com"; //Editable Based On Project

            public const string SSO_USER = "SSO_SYSTEM";
            public const string JoinString = ";";
            public const string SEND_ERROREMAIL = "Y";
            public const string DefaultLangID = "IN";
            public const string bDebugEmail = "Y";
            public const string SMTP = "172.31.254.247";
            public const string Generate_Random_Parameter = "12345";
            public const string byPassLogin = "N";
            public const string bSendEmailNotif = "Y";
            public const string NotifPublisherEmailSender = "no-reply@kalbenutritionals.com";
            public const string NotifPublisherEmailSubject = "DeskBooking:Notification"; //Editable Based On Project
            //DEV
            public const string bDefaultEmailNotif = "rizki.pamungkas@kalbenutritionals.com"; //Editable Based On Project
            public const string bOverideEmailNotif = "Y";
            public const string CONTENT_APPROVAL = "NOTIF APPROVAL";
            public const string CONTENT_APPROVED = "NOTIF APPROVED";
            public const string CONTENT_REJECTED = "NOTIF REJECTED";
            public const string NotifPublisherEmailSubject_Approval = "[DeskBooking]:Approval Notification"; //Editable Based On Project
            public const string NotifPublisherEmailSubject_Approved = "[DeskBooking]:Approved Notification"; //Editable Based On Project
            public const string NotifPublisherEmailSubject_Rejected = "[DeskBooking]:Rejected Notification"; //Editable Based On Project

            // Custom 
        }

        public class options
        {
            public const string NO = "N";
            public const string YES = "Y";
            public const string NO_Number = "0";
            public const string YES_Number = "1";
        }

        public class complaintComform
        {
            public const string CONFORM = "CONFORM";
            public const string NOT_CONFORM = "NOT CONFORM";
        }

        public class complaintCause
        {
            public const string CUSTOMER = "CUSTOMER";
            public const string HANDLING = "HANDLING";
            public const string MATERIAL = "MATERIAL";
            public const string NORMAL = "NORMAL";
            public const string PACKAGING = "PACKAGING";
            public const string PACKING_PROCESS = "PACKING PROCESS";
            public const string PRODUCTION_PROCESS = " PRODUCTION PROCESS";
        }

        public class role
        {
            public const string Administrator = "Administrator";
            public const string user = "user";
            public const string ADMIN_SU = "ADMIN-SU";

            // DECLARE ROLE CONSTANT IN THIS CLASS

        }


        public class statusTransaction
        {
            public const string New = "New";
            public const string Submitted = "Submitted";
            public const string Draft = "Draft";
            public const string WaitingApproval = "Waiting Approval";
            public const string Reject = "Reject";
            public const string Approved = "Approved";
            public const string Done = "Done";
        }

        public class mRunningNumberName
        {
            //FOR RUNNING NUMBER ORACLE
            public const string exampleRunningNumberFromNetCoreFramework = "exampleRunningNumberFromNetCoreFramework";
        }

        #endregion 

        #region "Math"
        public static int ParseToInteger(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return int.Parse(obj.ToString());
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }


        public static double ParseToDouble(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return double.Parse(obj.ToString().Trim());
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static decimal ParseToDecimal(object obj)
        {
            return ParseToDecimal(obj, 0, string.Empty, string.Empty);
        }


        public static decimal ParseToDecimal(object obj, int intCount, string txtCurrencyRounding, string txtCurrencyID)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    if (txtCurrencyID.Equals(string.Empty) | txtCurrencyRounding.Equals(string.Empty))
                    {
                        //Jika salah satu parameternya adalah kosong.
                        return decimal.Parse(obj.ToString().Trim());
                    }

                    if (txtCurrencyID.ToLower().Equals(txtCurrencyRounding.ToLower()))
                    {
                        //Jika matauang yang dipilih adalah matauang yang dibulatkan. 
                        return decimal.Parse(obj.ToString().Trim());
                    }
                    else
                    {
                        return decimal.Parse(obj.ToString());//  String.FormatNumber(obj.ToString().Trim(), intCount);
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public static bool ParseToBoolean(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        if (obj.Equals(DBNull.Value))
                        {
                            return false;
                        }

                        return bool.Parse(obj.ToString());
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ParseBooleanOracleToNET(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        if (obj.Equals(DBNull.Value))
                        {
                            return false;
                        }
                        else
                        {
                            if (obj.ToString().ToLower().Equals(options.YES.ToLower()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static string ParseBooleanNETToOracle(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return options.NO;
                }
                else
                {
                    try
                    {
                        if (obj.Equals(DBNull.Value))
                        {
                            return options.NO;
                        }
                        else
                        {
                            if (bool.Parse(obj.ToString()) == true)
                            {
                                return options.YES;
                            }
                            else
                            {
                                return options.NO;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return options.NO;
                    }
                }
            }
            catch (Exception ex)
            {
                return options.NO;
            }
        }

        public static DateTime ParseToDateTime(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return DATE_MINVALUE;
                }
                else
                {
                    return DateTime.Parse(obj.ToString());

                }
            }
            catch (Exception ex)
            {
                return DATE_MINVALUE;
            }
        }

        public static DateTime ParseToDateTimeTo(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return DATE_MAXVALUE;
                }
                else
                {
                    return DateTime.Parse(obj.ToString());

                }
            }
            catch (Exception ex)
            {
                return DATE_MAXVALUE;
            }
        }

        public static string ParseToFormatNumber(object obj, int intCount)
        {
            return ParseToFormatNumber(obj, intCount, "1", "2");
        }

        public static string ParseToFormatNumber(object obj, int intCount, string txtCurrencyRounding, string txtCurrencyID)
        {
            if (obj == null)
            {
                return decimal.Zero.ToString();
            }
            else
            {
                try
                {
                    if (txtCurrencyID.Equals(string.Empty) | txtCurrencyRounding.Equals(string.Empty))
                    {
                        //Jika salah satu parameternya adalah kosong.
                        return ParseToDecimal(obj.ToString()).ToString("N0", CultureInfo.InvariantCulture); // String.FormatNumber(obj, 0);
                    }

                    if (txtCurrencyID.ToLower().Equals(txtCurrencyRounding.ToLower()))
                    {
                        //Jika matauang yang dipilih adalah matauang yang dibulatkan. 
                        return ParseToDecimal(obj.ToString()).ToString("N0", CultureInfo.InvariantCulture); //String.FormatNumber(obj, 0);
                    }
                    else
                    {
                        return ParseToDecimal(obj.ToString()).ToString("N" + intCount, CultureInfo.InvariantCulture); //String.FormatNumber(obj, intCount);
                    }

                }
                catch (Exception ex)
                {
                    return decimal.Zero.ToString();
                }
            }
        }

        public static string ParseToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    return obj.ToString().Trim();
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
            }
        }
        #endregion
    }
}
