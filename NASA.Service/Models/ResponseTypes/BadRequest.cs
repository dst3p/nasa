using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASA.API.Models
{
    public class BadRequest
    {
        public BadRequest(string message, dynamic request)
        {
            this.message = message;
            this.request = request;
        }

        public dynamic request { get; set; }

        public string message { get; set; }
    }
}