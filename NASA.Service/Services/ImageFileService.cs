using NASA.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace NASA.API.Services
{
    public class ImageFileService
    {
        private readonly string _directory;

        public const string BaseSavePath = "C:/Nasa/Images/";

        public ImageFileService(string directory)
        {
            _directory = directory;
        }

        public ImageFileResponse HandleNasaResponse(NasaPhotoResponse nasaResponse)
        {
            // Create directory if it doesn't already exist
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            // Process each image concurrently
            Parallel.ForEach(nasaResponse.Photos, photo => SavePhoto(photo));

            return new ImageFileResponse
            {
                count = nasaResponse.Photos.Count,
                isSuccessful = true,
                location = _directory
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
                File.WriteAllBytes($"{_directory}/{fileName}", imageBytes);
            }
        }
    }
}