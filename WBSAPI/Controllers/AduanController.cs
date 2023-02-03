using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBSBE.BussLogic;
using WBSBE.Common.Model;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using WBSBE.Common.Library;
using System;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

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

        #region Download File
        [HttpPost]
        [Route("getAttachmentById")]
        public IActionResult downloadLampiran(AttachmentModel attachment)
        {
            try
            {
                var (fileType, archiveData, archiveName) = clsAduan.DownloadFiles(attachment.nomor);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

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
                var objData = clsAduan.GetDataById(dtParam.txtNomorID);

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
        [Route("ChangeStatusAduan")]
        public IActionResult deleteData(clsGlobalAPI apiDat)
        {
            JObject param = JObject.Parse(apiDat.objRequestData.ToString());
            var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
            apiDat.objData = clsAduan.Delete(dtParam.txtNomorID);
            return Ok(apiDat);
        }

        [HttpPost]
        [Route("SortAduan")]
        public IActionResult TestSorting(string? sortOrder)
        {
            ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
            ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
            ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";

            var aduan = clsAduan.sortingData(sortOrder);

            //return View(aduan.ToList()); //for UI
            return Ok(aduan); //for API
        }

        [HttpPost]
        [Route("SortSearchAduan")]
        public IActionResult TestSortingAndSearching(string? sortOrder, string? searchString)
        {
            ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
            ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
            ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";
            ViewData["CurrentFilter"] = searchString;

            var aduan = clsAduan.sortAndSearchByTextBox(sortOrder, searchString);

            //return View(aduan.ToList()); //for UI
            return Ok(aduan); //for API
        }

        [HttpPost]
        [Route("SearchAduanByButton")]
        public IActionResult SearchDataByButton (string? status, DateTime? awal, DateTime? akhir)
        {
            ViewData["StatusParam"] = String.IsNullOrEmpty(status) ? "status" : "";
            ViewData["FromDate"] = awal;
            ViewData["ToDate"] = akhir;

            var aduan = clsAduan.searchByButton(status, awal, akhir);

            //return View(aduan.ToList()); //for UI
            return Ok(aduan); //for API
        }

        [HttpPost]
        [Route("PaginationAduan")]
        public IActionResult TestPagination(string? sortOrder, string? currentFilter, string? searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
            ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
            ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var aduan = clsAduan.sortAndSearchByTextBox(sortOrder, searchString);

            int pageSize = 10;

            return View(PaginatedList<AduanModel>.CreateAsync(aduan.AsQueryable(), pageNumber ?? 1, pageSize));
        }
    }
}