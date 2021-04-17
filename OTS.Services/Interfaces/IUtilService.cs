using System.Collections.Generic;

namespace OTS.Services.Interfaces
{
    public interface IUtilService
    {
        IEnumerable<string> GetUrls(string searchEngine, int pageNumber, string searchPage);
        IEnumerable<string> GetFilteredUrls(IEnumerable<string> urls, string siteName);
    }
}
