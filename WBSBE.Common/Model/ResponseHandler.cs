using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.Code = "400";
            response.ResponseData = ex.Message;
            return response;
        }
        public static ApiResponse GetAppResponse(ResponseType type, object? contract, string message)
        {
            ApiResponse response;

            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "1";
                    response.Message = "Success";
                    response.ResponseData = message;

                    break;

                case ResponseType.Failure:
                    response.Code = "2";
                    response.Message = "Failure";
                    response.ResponseData = message;

                    break;
                case ResponseType.NotFound:
                    response.Code = "0";
                    response.Message = "No record available";
                    response.ResponseData = message;
                    break;
            }
            return response;
        }

        public static ApiResponse GetAppResponse(ResponseType type, object? contract)
        {
            return GetAppResponse(type, contract, null);
        }


        public static string SendResponse(string message)
        {
            return message;
        }
    }
}
