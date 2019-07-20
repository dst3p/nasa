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
        private string _path;

        public const string DefaultBasePath = "C:/Nasa/Images/";

        public ImageFileService(string directory = null)
        {
            _path = directory ?? DefaultBasePath;
        }

        /// <summary>
        /// Handles the response from the NASA API.
        /// </summary>
        /// <param name="nasaResponse"></param>
        /// <returns></returns>
        public ImageFileResponse HandleNasaResponse(NasaPhotoResponse nasaResponse)
        {
            var imageFileResponse = new ImageFileResponse
            {
                count = nasaResponse.Photos.Count,
                isSuccessful = false
            };

            if (!nasaResponse.Photos.Any())
            {
                return imageFileResponse;
            }

            // get the date from the first photo and put on the end of the path
            _path = $"{_path}/{nasaResponse.Photos[0].EarthDate:yyyyMMdd}";

            // Create directory if it doesn't already exist
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }

            // Process each image concurrently
            Parallel.ForEach(nasaResponse.Photos, photo => SavePhoto(photo));

            return new ImageFileResponse
            {
                count = nasaResponse.Photos.Count,
                isSuccessful = true,
                location = _path
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
                File.WriteAllBytes($"{_path}/{fileName}", imageBytes);
            }
        }
    }
}