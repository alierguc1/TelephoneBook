using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Shared.Models
{
    public class ResponseDatas<T>
    {
        public ResponseDatas(T data, int statusCode, bool isSuccessful)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
        }

        public ResponseDatas(int statusCode, bool isSuccessful, List<string> errors)
          : this(default(T), statusCode, isSuccessful)
        {
            Errors = errors;
        }

        public ResponseDatas(int statusCode, bool isSuccessful, string error)
          : this(default(T), statusCode, isSuccessful)
        {
            Errors = new List<string> { error };
        }

        public T Data { get; private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; private set; }

        internal IActionResult CreateResponse()
        {
            return new ObjectResult(this)
            {
                StatusCode = StatusCode
            };
        }

        public static IActionResult Success(T data, int statusCode)
        {
            return new ResponseDatas<T>(data, statusCode, true).CreateResponse();
        }
        public static IActionResult Success(int statusCode)
        {
            return new ResponseDatas<T>(default(T), statusCode, true).CreateResponse();
        }

        public static IActionResult Success(int statusCode, bool isOk)
        {
            return new ResponseDatas<T>(default(T), statusCode, isOk).CreateResponse();
        }

        public static IActionResult Fail(List<string> errors, int statusCode)
        {
            return new ResponseDatas<T>(statusCode, false, errors).CreateResponse();
        }

        public static IActionResult Fail(string error, int statusCode)
        {
            return new ResponseDatas<T>(statusCode, false, error).CreateResponse();
        }
    }
}
