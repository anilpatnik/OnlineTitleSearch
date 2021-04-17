using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using OTS.Services.Common;
using OTS.Services.Interfaces;

namespace OTS.Services
{
    public class UtilService : IUtilService
    {
        public IEnumerable<string> GetUrls(string searchEngine, int pageNumber, string searchPage)
        {
            var pageUri = string.Format(searchPage, searchEngine, pageNumber.ToString("00")); // Search page url

            using var client = new WebClient();

            var content = client.DownloadString(pageUri); // Call the search page and get the results in html string

            if (content.Length > 0)
            {
                // Find the html tag regular expression matched urls for processing

                var matches = Regex.Matches(content, AppConstants.CITE_PATTERN, RegexOptions.IgnoreCase);

                if (matches.Any())
                {
                    foreach (Match m in matches)
                    {
                        yield return m.Value;
                    }
                }
            }
        }

        public IEnumerable<string> GetFilteredUrls(IEnumerable<string> urls, string siteName)
        {
            var count = 0;

            foreach (var url in urls.Distinct())
            {
                count++;

                if (count > 50) break; // Exit if url appears in the next 50 results

                if (url.Contains(siteName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return count.ToString();
                }
            }
        }
    }
}
