using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WBSBE.BussLogic;
using WBSBE.Common.Entity.WBS;
using WBSBE.Common.Model;

namespace WBSBE.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class LookupController : Controller
    {
        #region Constructor  
        clsMLookup clsLookup;
        public LookupController()
        {
            clsLookup = new clsMLookup();
        }
        #endregion

        [HttpPost]
        [Route("GetAllLookup")]
        public IActionResult getAllData(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var objData = clsLookup.GetAllData();

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("UpdateLookup")]
        public IActionResult updateData([FromForm] mLookup lookup)
        {
            if (lookup.txtType == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Tipe pengelompokan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (lookup.txtName == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Nama data Lookup tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsLookup.Update(lookup);
                    return Ok(ResponseHandler.GetAppResponse(type, lookup, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }
    }
}
