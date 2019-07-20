using Newtonsoft.Json;
using System.Collections.Generic;

namespace NASA.API.Models
{
    public class NasaPhotoResponse
    {
        public NasaPhotoResponse()
        {
            Photos = new List<NasaPhoto>();
        }

        [JsonProperty("photos")]
        public IList<NasaPhoto> Photos { get; set; }
    }
}