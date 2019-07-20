using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASA.API.Models
{
    public class OkResponse<T>
    {
        public T result { get; set; }
    }
}