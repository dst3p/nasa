using Microsoft.VisualStudio.TestTools.UnitTesting;
using NASA.API.Models;
using NASA.API.Services;
using System;
using System.Collections.Generic;

namespace NASA.Tests
{
    [TestClass]
    public class ImageFileServiceTests
    {
        [TestMethod]
        public void CanSetDefaultSaveDirectory()
        {
            var imageFileService = new ImageFileService();

            Assert.AreEqual(ImageFileService.DefaultBasePath, imageFileService.BasePath);
        }

        [TestMethod]
        public void CanOverrideDefaultSaveDirectory()
        {
            var imageFileService = new ImageFileService("C:/");

            Assert.AreNotEqual(ImageFileService.DefaultBasePath, imageFileService.BasePath);
        }

        [TestMethod]
        public void CanSavePhotosFromResponseToDefaultDirectory()
        {
            var photoResponse = new NasaPhotoResponse
            {
                Photos = new List<NasaPhoto>
                {
                    new NasaPhoto
                    {
                        EarthDate = DateTime.Now,
                        Source = "https://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01000/opgs/edr/fcam/FLB_486265257EDR_F0481570FHAZ00323M_.JPG"
                    }
                }
            };

            var imageFileService = new ImageFileService();

            var imageFileResponse = imageFileService.HandleNasaResponse(photoResponse);

            Assert.IsTrue(imageFileResponse.location.StartsWith(ImageFileService.DefaultBasePath));
        }

        [TestMethod]
        public void CanSavePhotosFromResponseToSpecifiedDirectory()
        {
            var photoResponse = new NasaPhotoResponse
            {
                Photos = new List<NasaPhoto>
                {
                    new NasaPhoto
                    {
                        EarthDate = DateTime.Now,
                        Source = "https://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01000/opgs/edr/fcam/FLB_486265257EDR_F0481570FHAZ00323M_.JPG"
                    }
                }
            };

            var basePath = "C:/NASA/Images/OverrideSavePath/";

            var imageFileService = new ImageFileService(basePath);

            var imageFileResponse = imageFileService.HandleNasaResponse(photoResponse);

            Assert.IsTrue(imageFileResponse.location.StartsWith(basePath));
        }
    }
}