using Newtonsoft.Json;
using System.Collections.Generic;

namespace NASA.API.Models
{
    public class NasaPhotoResponse
    {
        [JsonProperty("photos")]
        public IList<NasaPhoto> Photos { get; set; }
    }
}