using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBSBE.BussLogic;
using WBSBE.Common.Model;
using WBSBE.DAL.Context;
using WBSBE.Common.Entity.WBS;

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

        #region 
        [HttpPost]
        [Route("GetHasilInvestigasi")]
        public IActionResult getResultInvestigation(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var dtParam = JsonConvert.DeserializeObject<mResultInvestigation>(param.ToString());

                WBSDBContext context = new WBSDBContext();
                var objData = clsInvestigation.GetDataById(dtParam.intResultInvestigationID, context);

                if (objData.message != null)
                {
                    ResponseType type = ResponseType.NotFound;
                    apiDat.txtMessage = objData.message;
                    return Ok(apiDat);
                }

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        #endregion

        #region DownloadAttachment
        [HttpPost]
        [Route("GetAttachmentById")]
        public FileResult DownloadFile(int id)
        {
            using (var context = new WBSDBContext())
            {
                var file = context.mAttachmentResult.Where(a => a.intAttachmentID == id && a.bitActive == true).FirstOrDefault();
                byte[] bytes = System.IO.File.ReadAllBytes(file.txtFilePath);
                return File(bytes, "application/octet-stream", file.txtFileName);
            }
        }
        #endregion

        #region AddAttachment
        [HttpPost]
        [Route("AddAttachment")]
        public IActionResult addLampiran([FromForm] ResultInvestigationModel investigation)
        {
            if (investigation.listDocument == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Lampiran tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsInvestigation.AddAttachment(investigation);
                    return Ok(ResponseHandler.GetAppResponse(type, investigation, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }
        #endregion

        #region DeleteAttachment
        [HttpPost]
        [Route("DeleteAttachment")]
        public IActionResult deleteLampiran(int idAttachment)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                string message = clsInvestigation.DeleteAttachment(idAttachment);
                return Ok(ResponseHandler.GetAppResponse(type, null, message));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        #endregion

        #region GetNoteHistory
        [HttpPost]
        [Route("GetNoteHistory")]
        public IActionResult getNoteHistory(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var dtParam = JsonConvert.DeserializeObject<mResultInvestigation>(param.ToString());

                WBSDBContext context = new WBSDBContext();
                var objData = clsInvestigation.GetDataById(dtParam.txtNomorID);

                if (objData.message != null)
                {
                    ResponseType type = ResponseType.NotFound;
                    apiDat.txtMessage = objData.message;
                    return Ok(apiDat);
                }

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
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
