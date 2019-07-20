using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNDYR.NASA
{
    public static class AppSettings
    {
        public static string ApiKey => ConfigurationManager.AppSettings["Nasa.ApiKey"];
    }
}
