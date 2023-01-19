using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBSBE.Common.Library.Interface;

namespace WBSBE.Common.Library
{
    public static class ServiceExtensions
    {
        //public static void ConfigureCors(this IServiceCollection services)
        //{
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("CorsPolicy",
        //            builder => builder.AllowAnyOrigin()
        //            .AllowAnyMethod()
        //            .AllowAnyHeader());
        //    });
        //}

        //public static void ConfigureIISIntegration(this IServiceCollection services)
        //{
        //    services.Configure<IISOptions>(options =>
        //    {

        //    });
        //}

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

    }
}
