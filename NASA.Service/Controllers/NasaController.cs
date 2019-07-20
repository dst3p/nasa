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
        /// <summary>
        /// Get images from the NASA API. Values passed through the
        /// imageRequest will be populated into a call to the NASA API.
        /// </summary>
        /// <param name="imageRequest">
        /// A NasaImageRequest defining the API key, the search date,
        /// the rover who took the images and an optional property
        /// to override the default image save path.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("images")]
        public IHttpActionResult GetImagesByDate([FromUri] NasaPhotoRequest imageRequest)
        {
            // Check model validity
            if (!ModelState.IsValid)
            {
                var badRequestResponse = new BadRequest("Request is malformed", imageRequest);

                return Content(HttpStatusCode.BadRequest, badRequestResponse);
            }

            var imageRepo = new NasaPhotoRepository("https://api.nasa.gov/mars-photos/api/v1/");

            // Get response from the service
            var response = imageRepo.Get(imageRequest);

            // Handle non-success states
            if (!response.IsSuccessful)
            {
                var responseObject = JsonConvert.DeserializeObject(response.Content);

                return Content(response.StatusCode, responseObject);
            }

            // Get an object from the response content
            var nasaImageResponse = JsonConvert.DeserializeObject<NasaPhotoResponse>(response.Content);

            // if the imageRequest.savePath is not populated, default it
            var savePath = imageRequest.savePath ?? $"{ImageFileService.BaseSavePath}{imageRequest.imageDate:yyyy-MM-dd}";

            // build the image path
            var imageFileService = new ImageFileService(savePath);

            // Get the image response
            try
            {
                return Ok(imageFileService.HandleNasaResponse(nasaImageResponse));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}