using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

//This is the optimize Generic model

namespace CustomerServiceWebAPI.Models
{
    public class OptimizeGenericModel<T>
    {
        public HttpStatusCode _statusCode { get; set; }
        public string _status { get; set; }
        public string _message { get; set; }
        public T _entityObject { get; set; }
        public Exception _ex { get; set; }
       

        /// <summary>
        /// Service Result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="hasValidationError"></param>
        public OptimizeGenericModel(HttpStatusCode statusCode, string status, string message, T entityObject, Exception exception = null)
        {
            _statusCode = statusCode;
            _status = status;
            _message = message;
            _entityObject = entityObject;
            _ex = exception;
        }
        
    }
}