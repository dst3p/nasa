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
        /// photoRequest will be populated into a call to the NASA API.
        /// </summary>
        /// <param name="photoRequest">
        /// A NasaPhotoRequest defining the API key, the search date,
        /// the rover that took the images and an optional property
        /// to override the default image save path.
        /// </param>
        /// <returns></returns>
        [HttpGet]
        [Route("photos")]
        public IHttpActionResult GetImagesByDate([FromUri] NasaPhotoRequest photoRequest)
        {
            // Check model validity
            if (!ModelState.IsValid)
            {
                var badRequestResponse = new BadRequest("Request is malformed", photoRequest);

                return Content(HttpStatusCode.BadRequest, badRequestResponse);
            }

            var imageRepo = new NasaPhotoRepository("https://api.nasa.gov/mars-photos/api/v1/");

            // Get response from the service
            var response = imageRepo.Get(photoRequest);

            // Handle non-success states
            if (!response.IsSuccessful)
            {
                var responseObject = JsonConvert.DeserializeObject(response.Content);

                return Content(response.StatusCode, responseObject);
            }

            // Get an object from the response content
            var nasaImageResponse = JsonConvert.DeserializeObject<NasaPhotoResponse>(response.Content);

            // build the image path
            var imageFileService = new ImageFileService(photoRequest.savePath);

            // Get the image response
            try
            {
                return Ok(imageFileService.HandleNasaResponse(nasaImageResponse));
            }
            catch (Exception e)
            {
                var exception = new Exception("Unable to save photos. View InnerException for more details.", e);

                return InternalServerError(exception);
            }
        }
    }
}