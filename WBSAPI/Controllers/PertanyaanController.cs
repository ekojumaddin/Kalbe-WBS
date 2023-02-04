using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WBSBE.BussLogic;
using WBSBE.Common.Model;

namespace WBSBE.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PertanyaanController : Controller
    {
        #region Constructor  
        clsMPertanyaan clsPertanyaan;
        public PertanyaanController()
        {
            clsPertanyaan = new clsMPertanyaan();
        }
        #endregion

        [HttpPost]
        [Route("GetAllPertanyaan")]
        public IActionResult getAllData(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var objData = clsPertanyaan.GetAllData();

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("CreatePertanyaan")]
        public IActionResult createData([FromForm] PertanyaanModel pertanyaan)
        {
            if (pertanyaan == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Data Master Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (pertanyaan.txtPertanyaan == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Nama Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (pertanyaan.intOrderPertanyaan == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Nomor urut Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsPertanyaan.Insert(pertanyaan);
                    return Ok(ResponseHandler.GetAppResponse(type, pertanyaan, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }

        [HttpPost]
        [Route("UpdatePertanyaan")]
        public IActionResult updateData([FromForm] PertanyaanModel pertanyaan)
        {
            if (pertanyaan == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Data Master Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (pertanyaan.txtPertanyaan == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Nama Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (pertanyaan.intPertanyaanID == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Nomor urut Pertanyaan tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsPertanyaan.Update(pertanyaan);
                    return Ok(ResponseHandler.GetAppResponse(type, pertanyaan, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }

        [HttpPost]
        [Route("ChangeStatusPertanyaan")]
        public IActionResult deleteData(clsGlobalAPI apiDat)
        {
            JObject param = JObject.Parse(apiDat.objRequestData.ToString());
            var dtParam = JsonConvert.DeserializeObject<PertanyaanModel>(param.ToString());
            apiDat.objData = clsPertanyaan.DeleteData(dtParam.intPertanyaanID);
            return Ok(apiDat);
        }
    }
}
