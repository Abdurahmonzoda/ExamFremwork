using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Response(T data)
        {
            Data = data;
            StatusCode = 200;
            Message = null;
        }
        public Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = (int)statusCode;
            Message = message;
        }
    }
}
