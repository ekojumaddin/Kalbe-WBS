using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WBSBE.BussLogic;
using WBSBE.Library;

namespace WBSBE.Controllers
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Test")]
    [Route("api/{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    //[CustomAuthorize]
    public class TestDBController : ControllerBase
    {
        ClsTestDBBL _ClsTestDBBL;
        public TestDBController()
        {
            _ClsTestDBBL = new ClsTestDBBL();

        }

        [HttpPost]
        public IActionResult GetAll(clsGlobalAPI apiDat)
        {
            try
            {
                var result = _ClsTestDBBL.GetAllData();
                apiDat = clsGlobalAPI.CreateResult(apiDat, true, result, string.Empty, string.Empty);
            }
            catch (Exception ex) 
            {
                apiDat = clsGlobalAPI.CreateError(apiDat, ex);
            }

            return Ok(apiDat);
        }
    }
}
