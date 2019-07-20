using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace NASA.API.Models
{
    public class NasaPhotoRequest
    {
        /// <summary>
        /// The key from NASA granting API access. 
        /// Can pass 'DEMO_KEY' for testing purpose, but this is subject to a lower rate limit.
        /// </summary>
        [Required]
        public string apiKey { get; set; }

        /// <summary>
        /// The rover that's taking the images.
        /// </summary>
        [Required]
        public string rover { get; set; }

        /// <summary>
        /// The date of the images. Searches the earth_date field
        /// from NASA.
        /// </summary>
        [Required]
        public DateTime imageDate { get; set; }

        /// <summary>
        /// An optional path where to save the images.
        /// If this field is empty, the files are saved to c:/NASA/Images/{date}
        /// </summary>
        public string savePath { get; set; }

        /// <summary>
        /// Creates the resource to pass to the API with the base path already defined.
        /// </summary>
        [JsonIgnore]
        public string resource => $"rovers/{rover}/photos?earth_date={imageDate:yyyy-M-d}&api_key={apiKey}";
    }
}