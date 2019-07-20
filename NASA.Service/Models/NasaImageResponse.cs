using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NASA.API.Models
{
    public class NasaPhotoResponse
    {
        [JsonProperty("photos")]
        public IList<NasaPhoto> Photos { get; set; }
    }

    public class NasaPhoto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("img_src")]
        public string Source { get; set; }

        [JsonProperty("earth_date")]
        public DateTime EarthDate { get; set; }
    }
}