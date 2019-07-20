using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WNDYR.NASA;

namespace WNDYR.Tests
{
    [TestClass]
    public class AppSettingsTests
    {
        [TestMethod]
        public void CanGetApiKey()
        {
            var apiKey = AppSettings.ApiKey;

            Assert.IsTrue(!string.IsNullOrEmpty(apiKey));
        }
    }
}
