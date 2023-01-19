using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using WBSBE.Common.ConfigurationModel;
using WBSBE.Common.Library.Interface;
using Microsoft.Extensions.Options;
using WBSBE.Common.Model.Custom.ModelAppGW;
using WBSBE.Common.Library;
using Newtonsoft.Json;
using KN2021_GlobalClient_NetCore;
using WBSBE.Common.Library.Request;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WBSBE.BussLogic.Custom
{
    public class ClsUserKnGlobalBl
    {
        private static AppSettings _appSettings;
        private static ICacheService _cacheService;

        public ClsUserKnGlobalBl(IOptions<AppSettings> appSettings, ICacheService cacheService)
        {
            _appSettings = appSettings.Value;
            _cacheService = cacheService;
        }

        public static mUser changeActiveRole(mRole paramData)
        {
            try
            {
                ClsGlobalClass.changeRole(paramData);
                mUser result = ClsGlobalClass.dLogin;
                result.activeRole = paramData;

                var diffInMinute = result.dtmExpired - DateTime.Now;
                PopulateMenu(result.activeRole.intRoleID, result.txtUserName, ClsGlobalClass.redisCode, diffInMinute);
                _cacheService.SetChangeValueAsync(result.txtBE_Token, JsonConvert.SerializeObject(result), diffInMinute);
                //result.txtKN2022_DeskBookingToken = generateJwtToken(result); //OLD
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static clsGlobalAPI Authenticate(AuthenticateRequest paramModel)
        {
            try
            {
                mUser result = new mUser();
                var varObj = new
                {
                    txtUsername = paramModel.Username,
                    txtPassword = paramModel.Password
                };

                //clsGlobalAPI apiDat = new clsGlobalAPI();
                ////mapping user
                //result.txtUserName = "sheilla.verisha";

                ////mapping ke role
                //var xxxx = new mRole();
                //xxxx.intRoleID = 274;
                //xxxx.intProgramID = 141;
                //result.ltActiveRoles.Add(xxxx);

                clsGlobalAPI apiDat = clsWSO2Helper.CallAPI(paramModel.Username, ClsGlobalConstant.DefaultLangID,
                                                            ClsGlobalConstant.SSO_CONST.WSO_checkUserPassword, varObj, ClsGlobalConstant.txtProgramCode, null);

                if (apiDat.bitError == true)
                {
                    throw new ArgumentException(apiDat.txtErrorMessage);
                }
                else if (apiDat.bitSuccess == true)
                {
                    var guid = Guid.NewGuid().ToString();
                    bool isHasActiveRole = false;
                    JObject resultDat = JObject.Parse(apiDat.objData.ToString());
                    result = JsonConvert.DeserializeObject<mUser>(resultDat["mUser"].ToString());

                    if (result == null) throw new ArgumentException("User not found");

                    result.ltRoles = JsonConvert.DeserializeObject<List<mRole>>(resultDat["roleList"].ToString());
                    if (result.ltRoles.Count == 1)
                    {
                        isHasActiveRole = true;
                        result.activeRole = result.ltRoles[0];
                        result.ltActiveRoles.Add(result.ltRoles[0]);// FOR SSO

                        //daftarin ke session biar bisa di akses
                        var session = new HttpContextAccessor();
                        session.HttpContext.Items["wSOToken"] = apiDat.txtToken;
                    }
                    else
                    {
                        result.activeRole = new mRole();
                    }
                    //result.txtKN2022_DeskBookingToken = generateJwtToken(result);

                    //    // insert into clsglobal API
                    DateTime dtNow = DateTime.Now.AddHours(8);
                    TimeSpan expired = TimeSpan.FromHours(8);
                    result.txtExpired = dtNow.ToString("dd-MM-yy HH:mm:ss");
                    result.dtmExpired = dtNow;
                    result.txtBE_Token = result.txtUserName + "_" + guid;
                    result.txtWso_Token = apiDat.txtToken;
                    apiDat.objData = JsonConvert.SerializeObject(result);

                    // result.activeRole.intRoleID = 274;

                    //    // daftarin redisnya  
                    _cacheService.SetChangeValueAsync(result.txtBE_Token, JsonConvert.SerializeObject(result), expired);
                    if (isHasActiveRole)
                    {
                        PopulateMenu(result.activeRole.intRoleID, result.txtUserName, result.txtBE_Token, expired);
                    }
                }

                return apiDat;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool logOut(mUser paramData)
        {
            try
            {
                _cacheService.DeleteCacheRedis(ClsGlobalClass.dLogin.txtBE_Token);
                _cacheService.DeleteCacheRedis(ClsGlobalClass.dLogin.txtBE_Token + "_menu");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void PopulateMenu(int intRoleID, string txtUserName, string txtGuid, TimeSpan? timeSpan = null)
        {
            var varObjMenu = new
            {
                intRoleID = intRoleID.ToString().Trim()
            };
            var result = JsonConvert.SerializeObject(varObjMenu, (Newtonsoft.Json.Formatting)System.Xml.Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            JObject jsonDat = JObject.Parse(result);
            clsGlobalAPI apiDatMenu = clsWSO2Helper.CallAPI(txtUserName, ClsGlobalConstant.DefaultLangID,
                                                                clsConstantClient.WSO_API.GetHierarchyOfMenu, jsonDat, ClsGlobalConstant.txtProgramCode, null);

            var param = JArray.Parse(apiDatMenu.objData.ToString());
            List<mMenu> List = JsonConvert.DeserializeObject<List<mMenu>>(param.ToString());

            var menuString = Newtonsoft.Json.JsonConvert.SerializeObject(List);

            _cacheService.SetChangeValueAsync(ClsGlobalConstant.reddisConst.reddisMenu.Replace("[REPLACE WITH GUID]", txtGuid), menuString, timeSpan);
        }

        #region Generate JWT
        private static string generateJwtToken(mUser paramMUser)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                                new Claim("muser",JsonConvert.SerializeObject(paramMUser))}),
                Expires = DateTime.UtcNow.AddDays(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region RegisteredProjectIntoAppGW
        public static bool SetupSystemConfigurationALL(string txtUserID, string txtLangId)
        {
            SetupConfiguration(txtUserID, txtLangId);
            return true;
        }

        public static bool SetupConfiguration(string txtUserID, string txtLangID)
        {
            ArrayList List = new()
            {
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.DefaultLangID, ClsGlobalConstant.key.DefaultLangID, ClsGlobalConstant.defaultConfigurationValue.DefaultLangID, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.ExceptionPublisherEmailSender, ClsGlobalConstant.key.ExceptionPublisherEmailSender, ClsGlobalConstant.defaultConfigurationValue.ExceptionPublisherEmailSender, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.ExceptionPublisherEmailSubject, ClsGlobalConstant.key.ExceptionPublisherEmailSubject, ClsGlobalConstant.defaultConfigurationValue.ExceptionPublisherEmailSubject, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.JoinString, ClsGlobalConstant.key.JoinString, ClsGlobalConstant.defaultConfigurationValue.JoinString, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.SEND_ERROREMAIL, ClsGlobalConstant.key.SEND_ERROREMAIL, ClsGlobalConstant.defaultConfigurationValue.SEND_ERROREMAIL, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.SenderEmail, ClsGlobalConstant.key.SenderEmail, ClsGlobalConstant.defaultConfigurationValue.SenderEmail, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.SHOW_STACKTRACE, ClsGlobalConstant.key.SHOW_STACKTRACE, ClsGlobalConstant.defaultConfigurationValue.SHOW_STACKTRACE, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.SMTP, ClsGlobalConstant.key.SMTP, ClsGlobalConstant.defaultConfigurationValue.SMTP, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.Generate_Random_Parameter, ClsGlobalConstant.key.Generate_Random_Parameter, ClsGlobalConstant.defaultConfigurationValue.Generate_Random_Parameter, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.bSendEmailNotif, ClsGlobalConstant.key.bSendEmailNotif, ClsGlobalConstant.defaultConfigurationValue.bSendEmailNotif, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.NotifPublisherEmailSender, ClsGlobalConstant.key.NotifPublisherEmailSender, ClsGlobalConstant.defaultConfigurationValue.NotifPublisherEmailSender, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.NotifPublisherEmailSubject, ClsGlobalConstant.key.NotifPublisherEmailSubject, ClsGlobalConstant.defaultConfigurationValue.NotifPublisherEmailSubject, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.bDefaultEmailNotif, ClsGlobalConstant.key.bDefaultEmailNotif, ClsGlobalConstant.defaultConfigurationValue.bDefaultEmailNotif, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.bOverideEmailNotif, ClsGlobalConstant.key.bOverideEmailNotif, ClsGlobalConstant.defaultConfigurationValue.bOverideEmailNotif, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.CONTENT_APPROVAL, ClsGlobalConstant.key.CONTENT_APPROVAL, ClsGlobalConstant.defaultConfigurationValue.CONTENT_APPROVAL, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.CONTENT_APPROVED, ClsGlobalConstant.key.CONTENT_APPROVED, ClsGlobalConstant.defaultConfigurationValue.CONTENT_APPROVED, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.CONTENT_REJECTED, ClsGlobalConstant.key.CONTENT_REJECTED, ClsGlobalConstant.defaultConfigurationValue.CONTENT_REJECTED, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.bDebugEmail, ClsGlobalConstant.key.bDebugEmail, ClsGlobalConstant.defaultConfigurationValue.bDebugEmail, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.NotifPublisherEmailSubject_Approval, ClsGlobalConstant.key.NotifPublisherEmailSubject_Approval, ClsGlobalConstant.defaultConfigurationValue.NotifPublisherEmailSubject_Approval, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.NotifPublisherEmailSubject_Approved, ClsGlobalConstant.key.NotifPublisherEmailSubject_Approved, ClsGlobalConstant.defaultConfigurationValue.NotifPublisherEmailSubject_Approved, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.NotifPublisherEmailSubject_Rejected, ClsGlobalConstant.key.NotifPublisherEmailSubject_Rejected, ClsGlobalConstant.defaultConfigurationValue.NotifPublisherEmailSubject_Rejected, ClsGlobalConstant.intProgramID),
                new mSysConfig(ClsGlobalConstant.MODULE_NAME, ClsGlobalConstant.key.byPassLogin, ClsGlobalConstant.key.byPassLogin, ClsGlobalConstant.defaultConfigurationValue.byPassLogin, ClsGlobalConstant.intProgramID)
            };

            clsGlobalAPI apiDat = clsWSO2Helper.CallAPI(txtUserID, ClsGlobalConstant.DefaultLangID,
                                                          clsConstantClient.WSO_API.SetupSysConfig, List, ClsGlobalConstant.txtProgramCode, null);

            if (apiDat.bitSuccess)
            {
                return true;
            }
            else
            {
                throw new ArgumentException(apiDat.txtErrorMessage);
            }
        }

        #endregion

    }
}
