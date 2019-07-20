using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace NASA.API.Models
{
    public class NasaImageRequest
    {
        public string apiKey { get; set; }

        public string rover { get; set; }

        public DateTime imageDate { get; set; }

        [JsonIgnore]
        public string resource => $"rovers/{rover}/photos?earth_date={imageDate:yyyy-M-d}&api_key={apiKey}";

        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(apiKey) ||
                    string.IsNullOrEmpty(rover) ||
                    imageDate.Equals(DateTime.MinValue)) return false;

                return true;
            }
        }
    }
}