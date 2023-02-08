//using KN2021_GlobalClient_NetCore;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using WBSBE.BussLogic;
//using WBSBE.Common.Model;
//using WBSBE.Common.Entity.WBS;
//using WBSBE.Common.Library;

//namespace WBSBE.Controllers
//{
//    [ApiController]
//    [Route("api/{version:apiVersion}/[controller]")]
//    [ApiVersion("1.0")]
//    public class Aduan2Controller : Controller
//    {
//        #region Constructor  
//        clsMAduan2 clsAduan;
//        public Aduan2Controller()
//        {
//            clsAduan = new clsMAduan2();
//        }
//        #endregion

//        [HttpPost]
//        [Route("GetAllAduan")]
//        public IActionResult getAllData(clsGlobalAPI apiDat)
//        {
//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var objData = clsAduan.GetAllData();

//                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
//                return Ok(apiDat);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
//            }
//        }

//        [HttpPost]
//        [Route("CreateAduan")]
//        public IActionResult createData([FromForm] AduanModel aduan)
//        {
//            if (aduan == null || aduan.fileData == null)
//            {
//                ResponseType type = ResponseType.Failure;
//                string message = "Data Aduan dan atau Bukti Pendukung tidak boleh kosong";
//                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
//            }
//            else if (aduan.txtPelapor == "Employee" && aduan.txtNIK == null)
//            {
//                ResponseType type = ResponseType.Failure;
//                string message = "Silahkan input data NIK";
//                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
//            }
//            else
//            {
//                try
//                {
//                    ResponseType type = ResponseType.Success;
//                    string message = clsAduan.Insert(aduan);
//                    return Ok(ResponseHandler.GetAppResponse(type, aduan, message));
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
//                }
//            }
//        }

//        [HttpPost]
//        [Route("InsertAduan2")]
//        public IActionResult insertData([FromForm] clsFormDataGlobalAPI apiDat)
//        {
//            JObject param = JObject.Parse(apiDat.Data.ToString());
//            clsGlobalAPI globalApi = JsonConvert.DeserializeObject<clsGlobalAPI>(param.ToString());
//            AduanModel aduan = new AduanModel();
//            aduan = JsonConvert.DeserializeObject<AduanModel>(globalApi.objRequestData.ToString());

//            if (apiDat.listFile1 != null)
//            {
//                aduan.fileData = apiDat.listFile1;
//            }

//            globalApi.objData = clsAduan.Insert(aduan);
//            return Ok(globalApi);
//        }


//        [HttpPost]
//        [Route("CheckAduan")]
//        public IActionResult check(clsGlobalAPI apiDat)
//        {
//            if (check == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
//                var objData = clsAduan.GetDataById(dtParam.txtNomorID);

//                if (objData.message != null)
//                {
//                    ResponseType type = ResponseType.NotFound;
//                    apiDat.txtMessage = objData.message;
//                    return Ok(apiDat);
//                }

//                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
//                return Ok(apiDat);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
//            }
//        }

//        [HttpPost]
//        [Route("UpdateAduan")]
//        public IActionResult updateData([FromForm] AduanModel aduan)
//        {
//            if (aduan == null || aduan.fileData == null)
//            {
//                ResponseType type = ResponseType.Failure;
//                string message = "Data Aduan dan atau Bukti Pendukung tidak boleh kosong";
//                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
//            }
//            else if (aduan.txtPelapor == "Employee" && aduan.txtNIK == null)
//            {
//                ResponseType type = ResponseType.Failure;
//                string message = "Silahkan input data NIK";
//                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
//            }
//            else if (aduan.txtPertanyaan1 == null || aduan.txtPertanyaan2 == null || aduan.txtPertanyaan3 == null || aduan.txtPertanyaan4 == null)
//            {
//                ResponseType type = ResponseType.Failure;
//                string message = "Semua pertanyaan wajib dijawab";
//                return BadRequest(ResponseHandler.GetAppResponse(type, null, message));
//            }
//            else
//            {
//                try
//                {
//                    ResponseType type = ResponseType.Success;
//                    string message = clsAduan.Update(aduan);
//                    return Ok(ResponseHandler.GetAppResponse(type, aduan, message));
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ResponseHandler.GetExceptionResponse(ex));
//                }
//            }
//        }

//        [HttpPost]
//        [Route("ViewAduan")]
//        public IActionResult view(clsGlobalAPI apiDat)
//        {
//            if (check == null)
//            {
//                return BadRequest();
//            }

//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
//                var objData = clsAduan.GetDataById(dtParam.txtNomorID);

//                if (objData.message != null)
//                {
//                    ResponseType type = ResponseType.NotFound;
//                    apiDat.txtMessage = objData.message;
//                    return Ok(apiDat);
//                }

//                apiDat = clsGlobalAPI.CreateResult(apiDat, true, objData, string.Empty, string.Empty);
//                return Ok(apiDat);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
//            }
//        }

//        [HttpPost]
//        [Route("ChangeStatusAduan")]
//        public IActionResult deleteData(clsGlobalAPI apiDat)
//        {
//            JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//            var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
//            apiDat.objData = clsAduan.Delete(dtParam.txtNomorID);
//            return Ok(apiDat);
//        }

//        #region Download File
//        [HttpPost]
//        [Route("getAttachment")]
//        public IActionResult downloadLampiran(clsGlobalAPI apiDat)
//        {
//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var dtParam = JsonConvert.DeserializeObject<AttachmentModel>(param.ToString());
//                var (fileType, archiveData, archiveName) = clsAduan.DownloadFiles(dtParam.nomor);
//                return File(archiveData, fileType, archiveName);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }

//        }
//        #endregion

//        [HttpPost]
//        [Route("SortAduan")]
//        public IActionResult TestSorting(string sortOrder)
//        {
//            ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
//            ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
//            ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";

//            var aduan = clsAduan.sortingData(sortOrder);
//            return View(aduan.ToList());
//        }

//        [HttpPost]
//        [Route("SortSearchAduan")]
//        public IActionResult TestSortingAndSearching(clsGlobalAPI apiDat)
//        {
//            //ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
//            //ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
//            //ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";
//            //ViewData["CurrentFilter"] = searchString;

//            //var aduan = clsAduan.searchingData(sortOrder, searchString);

//            //return View(aduan.ToList());
//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var dtParam = JsonConvert.DeserializeObject<SortModel>(param.ToString());
//                apiDat.objData = clsAduan.FilterData(dtParam.status, dtParam.dateStart, dtParam.dateEnd);
//                apiDat.bitSuccess = true;
//                apiDat.txtMessage = "Get Attachment";
//            }
//            catch (Exception ex)
//            {
//                apiDat.txtErrorMessage = ex.Message;
//                apiDat.txtStackTrace = ex.StackTrace;
//                apiDat.txtMessage = ex.Message;
//                apiDat.bitSuccess = false;
//                apiDat.bitError = true;
//            }
//            return Ok(apiDat);

//        }

//        [HttpPost]
//        [Route("GetAttachmentById")]
//        public IActionResult getAttachment(clsGlobalAPI apiDat)
//        {
//            try
//            {
//                JObject param = JObject.Parse(apiDat.objRequestData.ToString());
//                var dtParam = JsonConvert.DeserializeObject<AduanModel>(param.ToString());
//                apiDat.objData = clsAduan.GetAttachmentById(dtParam.txtNomorID);
//                apiDat.bitSuccess = true;
//                apiDat.txtMessage = "Get Attachment";
//            }
//            catch (Exception ex)
//            {
//                apiDat.txtErrorMessage = ex.Message;
//                apiDat.txtStackTrace = ex.StackTrace;
//                apiDat.txtMessage = ex.Message;
//                apiDat.bitSuccess = false;
//                apiDat.bitError = true;
//            }
//            return Ok(apiDat);
//        }

//        [HttpPost]
//        [Route("PaginationAduan")]
//        public IActionResult TestPagination(string sortOrder, string currentFilter, string searchString, int? pageNumber)
//        {
//            ViewData["CurrentSort"] = sortOrder;
//            ViewData["NomoAduanParam"] = String.IsNullOrEmpty(sortOrder) ? "nomor" : "";
//            ViewData["Pertanyaan1Param"] = String.IsNullOrEmpty(sortOrder) ? "pertanyaan" : "";
//            ViewData["StatusParam"] = String.IsNullOrEmpty(sortOrder) ? "status" : "";

//            if (searchString != null)
//            {
//                pageNumber = 1;
//            }
//            else
//            {
//                searchString = currentFilter;
//            }

//            ViewData["CurrentFilter"] = searchString;

//            var aduan = clsAduan.searchingData(sortOrder, searchString);

//            int pageSize = 10;

//            return View(PaginatedList<AduanModel>.CreateAsync(aduan.AsQueryable(), pageNumber ?? 1, pageSize));
//        }

//    }
//}