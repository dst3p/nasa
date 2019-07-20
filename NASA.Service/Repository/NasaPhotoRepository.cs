using NASA.API.Models;
using RestSharp;

namespace NASA.API.Repository
{
    public class NasaPhotoRepository
    {
        private string _baseEndpoint;

        public NasaPhotoRepository(string baseEndpoint) => _baseEndpoint = baseEndpoint;

        /// <summary>
        /// Executes a web request against the NASA API.
        /// </summary>
        /// <param name="photoRequest"></param>
        /// <returns></returns>
        public IRestResponse Get(NasaPhotoRequest photoRequest)
        {
            var client = new RestClient(_baseEndpoint);
            var request = new RestRequest(photoRequest.resource);

            request.AddHeader("Accept", "application/json");

            return client.Execute(request);
        }
    }
}