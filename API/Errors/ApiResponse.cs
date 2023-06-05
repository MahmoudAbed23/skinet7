using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMassage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMassage(int statusCode)
        {
            return statusCode switch
            {
                400 => "bad request",
                401 => "not authorized",
                404 => "not found",
                500 => "internal error",
                _ => null
            };
        }
    }
}