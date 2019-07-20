using NASA.API.Models;
using RestSharp;

namespace NASA.API.Repository
{
    public class NasaPhotoRepository
    {
        private string _baseEndpoint;

        public NasaPhotoRepository(string baseEndpoint)
        {
            _baseEndpoint = baseEndpoint;
        }

        public IRestResponse Get(NasaPhotoRequest imageRequest)
        {
            var client = new RestClient(_baseEndpoint);
            var request = new RestRequest(imageRequest.resource);

            request.AddHeader("Accept", "application/json");

            return client.Execute(request);
        }
    }
}