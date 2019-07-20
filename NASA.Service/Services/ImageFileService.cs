using NASA.API.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NASA.API.Services
{
    public class ImageFileService
    {
        public readonly string BasePath;

        private DateTime EarthDate { get; set; }

        public const string DefaultBasePath = "C:/Nasa/Images/";

        public ImageFileService(string directory = null)
        {
            BasePath = directory ?? DefaultBasePath;
        }

        /// <summary>
        /// Handles the response from the NASA API.
        /// </summary>
        /// <param name="nasaResponse"></param>
        /// <returns></returns>
        public ImageFileResponse HandleNasaResponse(NasaPhotoResponse nasaResponse)
        {
            if (!nasaResponse.Photos.Any())
            {
                return null;
            }

            // get the date from the first photo
            EarthDate = nasaResponse.Photos[0].EarthDate;


            // Create directory if it doesn't already exist
            if (!System.IO.Directory.Exists($"{BasePath}/{EarthDate:yyyyMMdd}"))
            {
                System.IO.Directory.CreateDirectory($"{BasePath}/{EarthDate:yyyyMMdd}");
            }

            // Process each image concurrently
            Parallel.ForEach(nasaResponse.Photos, photo => SavePhoto(photo));

            return new ImageFileResponse
            {
                count = nasaResponse.Photos.Count,
                isSuccessful = true,
                location = BasePath
            };
        }

        private void SavePhoto(NasaPhoto p)
        {
            using (var webClient = new WebClient())
            {
                // Get the image bytes from the image link in the photo response
                byte[] imageBytes = webClient.DownloadData(p.Source);

                // Get the last fragment of the url which is the filename
                var fileName = p.Source.Split('/').Last();

                // Write the file to the directory
                File.WriteAllBytes($"{BasePath}/{EarthDate:yyyyMMdd}/{fileName}", imageBytes);
            }
        }
    }
}