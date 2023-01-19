using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WBSBE.Common.Library;

namespace WBSBE.Library
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = ClsGlobalClass.dLogin;

            if (user == null)
            {
                clsGlobalAPI result = new clsGlobalAPI();
                result.bitSuccess = false;
                result.bitError = true;
                result.txtErrorMessage = "Unauthorized [From Attribute]";

                // not logged in
                context.Result = new JsonResult(result) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
