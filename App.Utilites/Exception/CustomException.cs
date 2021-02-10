using System;
using System.Collections.Generic;
using System.Net;

namespace App.Utilites.Exceptions
{
    public class CustomException:Exception
    {
        public CustomException(string msg) : base(msg) { 
        
        }

    }

    public class HttpStatusException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }


    public class NotFoundException : KeyNotFoundException
    {
        public NotFoundException(string msg) : base(msg)
        {

        }

    }
    public class MyUnauthException: Exception
    {
        public MyUnauthException(string msg) : base(msg)
        {

        }

    }
}
