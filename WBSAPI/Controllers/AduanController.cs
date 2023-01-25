using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBSBE.BussLogic;
using WBSBE.Common.Model;
using WBSBE.Common.Entity.WBS;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace WBSBE.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class AduanController : Controller
    {
        #region Constructor  
        clsMAduan clsAduan;
        public AduanController()
        {
            clsAduan = new clsMAduan();
        }
        #endregion

        [HttpPost]
        [Route("GetAllAduan")]
        public IActionResult getAllData(clsGlobalAPI apiDat)
        {
            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var objData = clsAduan.GetAllData();

                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
                return Ok(apiDat);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("CreateAduan")]
        public IActionResult createData([FromForm] AduanModel aduan)
        {
            if (aduan == null || aduan.fileData == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Data Aduan dan atau Bukti Pendukung tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (aduan.txtPelapor == "Employee" && aduan.txtNIK == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Silahkan input data NIK";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (aduan.txtPertanyaan1 == null || aduan.txtPertanyaan2 == null || aduan.txtPertanyaan3 == null || aduan.txtPertanyaan4 == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Semua pertanyaan wajib dijawab";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsAduan.Insert(aduan);
                    return Ok(ResponseHandler.GetAppResponse(type, aduan, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }

        [HttpPost]
        [Route("CheckAduan")]
        public IActionResult check(clsGlobalAPI apiDat)
        {
            if (check == null)
            {
                return BadRequest();
            }

            try
            {
                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
                var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
                var aduan = clsAduan.GetDataById(dtParam.txtNomorID);

                if (aduan.message != null)
                {
                    ResponseType type = ResponseType.NotFound;
                    apiDat.txtMessage = aduan.message;
                    return Ok(apiDat);
                }

                return Ok(aduan);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("UpdateAduan")]
        public IActionResult updateData([FromForm] AduanModel aduan)
        {
            if (aduan == null || aduan.fileData == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Data Aduan dan atau Bukti Pendukung tidak boleh kosong";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (aduan.txtPelapor == "Employee" && aduan.txtNIK == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Silahkan input data NIK";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else if (aduan.txtPertanyaan1 == null || aduan.txtPertanyaan2 == null || aduan.txtPertanyaan3 == null || aduan.txtPertanyaan4 == null)
            {
                ResponseType type = ResponseType.Failure;
                string message = "Semua pertanyaan wajib dijawab";
                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
            }
            else
            {
                try
                {
                    ResponseType type = ResponseType.Success;
                    string message = clsAduan.Update(aduan);
                    return Ok(ResponseHandler.GetAppResponse(type, aduan, message));
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }

        [HttpPost]
        [Route("DeleteAduan")]
        public IActionResult deleteData(clsGlobalAPI apiDat)
        {
            JObject param = JObject.Parse(apiDat.objRequestData.ToString());
            var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
            apiDat.objData = clsAduan.Delete(dtParam.txtNomorID);
            return Ok(apiDat);
        }
    }
}