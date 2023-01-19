using KN2021_GlobalClient_NetCore;
using Swashbuckle.AspNetCore.Filters;

namespace WBSBE.Library.SwaggerAttribute
{
    public class Default_Example : IExamplesProvider<clsGlobalAPI>
    {
        public clsGlobalAPI GetExamples()
        {
            return new clsGlobalAPI
            {
                txtUsername = "Rizki.Pamungkas1",
                objRequestData = new object()
            };
        }
    }
}
