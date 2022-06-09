using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Areas.api
{
    public class ApiResponse<T> where T : class
    {
        public ApiResponse(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        } 
        public ApiResponse<T> GetApiResponse() {
            return this;
        }
         public T Data { get; private set; }
         public bool Success { get; private set; } 
         public string Message { get; private set; }
    }
}
