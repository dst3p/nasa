using NASA.API.Models;
using NASA.API.Repository;
using NASA.API.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Http;

namespace NASA.API.Controllers
{
    [RoutePrefix("nasa")]
    public class NasaController : ApiController
    {
        private const string BaseImageSavePath = "C:/Nasa/Images/";

        [HttpGet]
        [Route("images")]
        public IHttpActionResult GetImagesByDate([FromUri] NasaImageRequest imageRequest)
        {
            // Check model validity
            if (!ModelState.IsValid || !imageRequest.IsValid)
            {
                var badRequestResponse = new BadRequest("Request is malformed", imageRequest);

                return Content(HttpStatusCode.BadRequest, badRequestResponse);
            }

            var imageRepo = new NasaImageRepository("https://api.nasa.gov/mars-photos/api/v1/");
                
            // Get response from the service
            var response = imageRepo.Get(imageRequest);

            // Handle success
            if (response.IsSuccessful)
            {
                // Get an object from the response content
                var nasaImageResponse = JsonConvert.DeserializeObject<NasaPhotoResponse>(response.Content);

                // build the image path
                var imageFileService = new ImageFileService($"{BaseImageSavePath}{imageRequest.imageDate:yyyy-MM-dd}");

                // Get the image response
                return Ok(imageFileService.HandleNasaResponse(nasaImageResponse));
            }

            var responseObject = JsonConvert.DeserializeObject(response.Content);

            return Content(response.StatusCode, responseObject);
        }
    }
}
