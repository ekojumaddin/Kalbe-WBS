using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.ConfigurationModel;

namespace WBSBE.Common.Library
{
    public static class AppServicesHelper
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        ///// Provides static access to the current HttpContext
        ///// </summary>
        //public static HttpContext HttpContext_Current
        //{
        //    get
        //    {
        //        IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        //        return httpContextAccessor?.HttpContext;
        //    }
        //}

        //public static IHostingEnvironment HostingEnvironment
        //{
        //    get
        //    {
        //        return services.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
        //    }
        //}

        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static AppSettings Config
        {
            get
            {
                //This works to get file changes.
                var s = services.GetService(typeof(IOptionsMonitor<AppSettings>)) as IOptionsMonitor<AppSettings>;
                AppSettings config = s.CurrentValue;

                return config;
            }
        }

        public static AppConnectionString getConnetionString
        {
            get
            {
                //This works to get file changes.
                var s = services.GetService(typeof(IOptionsMonitor<AppConnectionString>)) as IOptionsMonitor<AppConnectionString>;
                AppConnectionString config = s.CurrentValue;

                return config;
            }
        }

        public static WSOHelperModel getWSOHelperConfiguration
        {
            get
            {
                //This works to get file changes.
                var s = services.GetService(typeof(IOptionsMonitor<WSOHelperModel>)) as IOptionsMonitor<WSOHelperModel>;
                WSOHelperModel config = s.CurrentValue;

                return config;
            }
        }

    }
}
