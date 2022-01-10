using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;

namespace CustomerServiceWebAPI.Models
{
    public class GenericModel<T>
    {
        private HttpStatusCode _statusCode;
        private string _status;
        private string _message;
        private T _entityObject;
        private Exception _ex;

        public HttpStatusCode StatusCode
        {
            get
            {
                return this._statusCode;
            }
            set
            {
                this._statusCode = value;
            }
        }
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }
        
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
        public T EntityObject
        {
            get
            {
                return this._entityObject;
            }
            set
            {
                this._entityObject = value;
            }
        }
        public Exception Exception
        {
            get
            {
                return this._ex;
            }
            set
            {
                this._ex = value;
            }
        }
    }
}