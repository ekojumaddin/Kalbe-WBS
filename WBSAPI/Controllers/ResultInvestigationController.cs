using Microsoft.AspNetCore.Mvc;
using WBSBE.BussLogic;
using WBSBE.Common.Model;

namespace WBSBE.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class ResultInvestigationController : Controller
    {
        #region Constructor  
        clsMResultInvestigation clsInvestigation;
        public ResultInvestigationController()
        {
            clsInvestigation = new clsMResultInvestigation();
        }
        #endregion
        #region SaveResultInvestigation
        [HttpPost]
        [Route("SaveResultInvestigation")]
        public IActionResult saveData([FromForm] ResultInvestigationModel investigation)
        {
            if (investigation == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Laporan Hasil Investigasi tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsInvestigation.Insert(investigation);
                    return Ok(ResponseHandler.GetAppResponse(type, investigation, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }
        #endregion

        #region SubmitResultInvestigation
        [HttpPost]
        [Route("SubmitResultInvestigation")]
        public IActionResult submitData([FromForm] ResultInvestigationModel investigation)
        {
            if (investigation == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Laporan Hasil Investigasi tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsInvestigation.Submit(investigation);
                    return Ok(ResponseHandler.GetAppResponse(type, investigation, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }
        #endregion
    }
}
