using System.Configuration;

namespace WNDYR.NASA
{
    public static class AppSettings
    {
        public static string ApiKey => ConfigurationManager.AppSettings["Nasa.ApiKey"];
    }
}