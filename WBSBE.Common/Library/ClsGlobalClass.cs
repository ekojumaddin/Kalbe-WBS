using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WBSBE.Common.Library.Interface;
using WBSBE.Common.Model.Custom.ModelAppGW;

namespace WBSBE.Common.Library
{
    public class ClsGlobalClass
    {
        #region Configure Context
        private static ICacheService _cacheService;
        private static IHttpContextAccessor _httpContextAccessor;
        private static IWebHostEnvironment _HostingEnvironment;

        public static void Configure(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment HostingEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _HostingEnvironment = HostingEnvironment;
        }

        public static void ConfigureCache(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
        public static string GetBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}";
        public static string GetRootPath => _HostingEnvironment.ContentRootPath;

        #endregion


        public static mUser ExtractRedisCodeToObject(string token)
        {
            try
            {
                var value = _cacheService.GetCacheValueAsync(token);

                var jsonString = value.Result;
                if (!string.IsNullOrEmpty(jsonString))
                {
                    mUser data = new mUser();
                    JObject resultDat = JObject.Parse(ClsGlobalConstant.ParseToString(jsonString));
                    data = JsonConvert.DeserializeObject<mUser>(resultDat.ToString());

                    if (data.dtmExpired < DateTime.Now)
                    {
                        _cacheService.DeleteCacheRedis(token);
                        _cacheService.DeleteCacheRedis(token + "_menu");
                        throw new UnauthorizedAccessException("Session sudah habis silahkan re login");
                    }


                    data.activeRole = data.ltActiveRoles.FirstOrDefault(x => x.intProgramID == ClsGlobalConstant.intProgramID);
                    return data;
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void changeRole(mRole param)
        {
            var dt = (mUser)_httpContextAccessor.HttpContext.Items["mUser"];
            dt.activeRole = param;
            var dtActiveOnList = dt.ltActiveRoles.Where(x => x.intProgramID == ClsGlobalConstant.intProgramID).FirstOrDefault();

            if (dtActiveOnList == null)
            {
                dt.ltActiveRoles.Add(param);
            }
            else
            {
                // Satu sessiion aktif hanya punya 1 role di setiap program
                dt.ltActiveRoles.Remove(dtActiveOnList);
                dt.ltActiveRoles.Add(param);
            }

            _httpContextAccessor.HttpContext.Items["mUser"] = dt;
        }

        public static mUser dLogin
        {
            get
            {
                return (mUser)_httpContextAccessor.HttpContext.Items["mUser"];
            }
        }

        public static string wsoToken
        {
            get
            {

                return (string)_httpContextAccessor.HttpContext.Items["wSOToken"];
            }
        }

        public static string redisCode
        {
            get
            {

                return (string)_httpContextAccessor.HttpContext.Items["redisCode"];
            }
        }
    }
}
