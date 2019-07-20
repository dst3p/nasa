using Newtonsoft.Json;
using System;

namespace NASA.API.Models
{
    public class NasaPhoto
    {
        [JsonProperty("img_src")]
        public string Source { get; set; }

        [JsonProperty("earth_date")]
        public DateTime EarthDate { get; set; }
    }
}