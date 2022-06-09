using System.Net;
using System.Runtime.Serialization;

namespace GoSmartValue.Web.Areas.api.Models
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember]
        public string Version { get { return ApiVersionConstants.Default; } }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public dynamic Data { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        public ApiResponse(HttpStatusCode statusCode, dynamic data = null, string errorMessage = null)
        {
            StatusCode = (int)statusCode;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
