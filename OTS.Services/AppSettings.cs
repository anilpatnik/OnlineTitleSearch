using Microsoft.Extensions.Configuration;
using OTS.Services.Common;
using OTS.Services.Interfaces;

namespace OTS.Services
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _config;

        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        public string SiteName => _config["SiteName"].IsNullEmptyDefault(AppConstants.SITE_NAME);
        public int MaxPages => _config["MaxPages"].IsNullEmptyDefault(10);
        public string SearchPage => _config["SearchPage"];
    }
}
