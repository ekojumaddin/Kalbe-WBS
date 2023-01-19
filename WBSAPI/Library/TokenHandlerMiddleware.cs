using Microsoft.Extensions.Options;
using WBSBE.BussLogic.Custom;
using WBSBE.Common.ConfigurationModel;
using WBSBE.Common.Library;
using WBSBE.Common.Model.Custom.ModelAppGW;

namespace WBSBE.Library
{
    public class TokenHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public TokenHandlerMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ClsUserKnGlobalBl userService)
        {
            var token = context.Request.Headers["BEAuthorization"].FirstOrDefault()?.Split(" ").Last();
            var wSOToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            if (token != null)
                attachUserToContext(context, userService, token, wSOToken);


            await _next(context);
        }

        private void attachUserToContext(HttpContext context, ClsUserKnGlobalBl userService, string token, string wSOToken)
        {
            try
            {
                mUser userDt = ClsGlobalClass.ExtractRedisCodeToObject(token);
                context.Items["mUser"] = userDt;
                context.Items["wSOToken"] = userDt.txtWso_Token;
                context.Items["redisCode"] = token;

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                //tokenHandler.ValidateToken(token, new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(key),
                //    ValidateIssuer = false,
                //    ValidateAudience = false,
                //    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //    ClockSkew = TimeSpan.Zero
                //}, out SecurityToken validatedToken);

                //var jwtToken = (JwtSecurityToken)validatedToken;
                //var userDt = JsonConvert.DeserializeObject<mUser>((jwtToken.Claims.First(x => x.Type == "muser").Value));
                //context.Items["mUser"] = userDt;
                //context.Items["wSOToken"] = wSOToken;

            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
