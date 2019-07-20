using NASA.API.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NASA.API.Repository
{
    public class NasaImageRepository
    {
        private string _baseEndpoint;

        public NasaImageRepository(string baseEndpoint)
        {
            _baseEndpoint = baseEndpoint;
        }

        public IRestResponse Get(NasaImageRequest imageRequest)
        {
            var client = new RestClient(_baseEndpoint);
            var request = new RestRequest(imageRequest.resource);

            request.AddHeader("Accept", "application/json");

            return client.Execute(request);
        }
    }
}