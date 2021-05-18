﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VOD.Common.Exceptions
{
   public class HttpResponseException : Exception
    {
        //Properties
        public HttpStatusCode HttpStatusCode { get; }
        public object ValidationErrors { get; }

        //Constructors
        public HttpResponseException(HttpStatusCode status, string message, object validationErrors) : base(message)
        {
            HttpStatusCode = status;
            ValidationErrors = validationErrors;
        }

        public HttpResponseException(HttpStatusCode status, string message) : this(status, message, null)
        {                       
        }

        public HttpResponseException(HttpStatusCode status) : this(status, string.Empty, null)
        {          
        }

    }
}
