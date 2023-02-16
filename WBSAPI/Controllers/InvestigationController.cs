using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBSBE.BussLogic;
using WBSBE.Common.Model;

namespace WBSBE.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class InvestigationController : Controller
    {
        #region Constructor  
        clsMSetInvestigation clsInvestigation;
        public InvestigationController()
        {
            clsInvestigation = new clsMSetInvestigation();
        }
        #endregion

        #region GetAllUser
        [HttpPost]
        [Route("GetAllUser")]
        public IActionResult getAllUser(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var objData = clsInvestigation.GetAllUser();

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        #endregion

        #region GetAllRole
        [HttpPost]
        [Route("GetAllRole")]
        public IActionResult getAllRole(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var objData = clsInvestigation.GetAllRole();

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        #endregion

        #region SaveTeamInvestigation
        [HttpPost]
        [Route("SaveTeamInvestigation")]
        public IActionResult saveData([FromForm] SetTeamInvestigationModel investigation)
        {
            if (investigation == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Tim Investigation tidak boleh kosong";
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

        #region SubmitTeamInvestigation
        [HttpPost]
        [Route("SubmitTeamInvestigation")]
        public IActionResult submitData([FromForm] SetTeamInvestigationModel investigation)
        {
            if (investigation == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Tim Investigation tidak boleh kosong";
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

        #region GetAllAduanByUserId
        [HttpPost]
        [Route("GetAllAduanByUserId")]
        public IActionResult GetAllAduanByUserId(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var dtParam = JsonConvert.DeserializeObject<SetTeamInvestigationModel>(param.ToString());

                if (dtParam.intUserID.HasValue)
                {
                    var objData = clsInvestigation.GetAllAduanById((int)dtParam.intUserID);
                    apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);                    
                }

                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        #endregion
    }
}
