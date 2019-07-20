using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NASA.API.Models;
using NASA.API.Repository;

namespace NASA.Tests
{
    [TestClass]
    public class NasaPhotoRepositoryTests
    {
        [TestMethod]
        public void CanValidatePhotoRequestModel()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void CanGetOkStatusForCuriosity()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "curiosity"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response.IsSuccessful);
        }

        [TestMethod]
        public void CanGetOkStatusForOpportunity()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "opportunity"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response.IsSuccessful);
        }

        [TestMethod]
        public void CanGetOkStatusForSpirit()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "spirit"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response.IsSuccessful);
        }

        [TestMethod]
        public void CanGetPhotoResponseForCuriosity()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "curiosity"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response?.Content.Length > 0);
        }

        [TestMethod]
        public void CanGetPhotoResponseForOpportunity()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "opportunity"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response?.Content.Length > 0);
        }

        [TestMethod]
        public void CanGetPhotoResponseForSpirit()
        {
            // Arrange
            var nasaPhotoRequest = new NasaPhotoRequest
            {
                apiKey = "DEMO_KEY",
                imageDate = DateTime.Today,
                rover = "spirit"
            };

            var baseEndpoint = "https://api.nasa.gov/mars-photos/api/v1/";

            // Act
            var photoRepository = new NasaPhotoRepository(baseEndpoint);

            var response = photoRepository.Get(nasaPhotoRequest);

            // Assert
            Assert.IsTrue(response?.Content.Length > 0);
        }
    }
}