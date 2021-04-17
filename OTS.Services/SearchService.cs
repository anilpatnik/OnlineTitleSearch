using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OTS.Services.Interfaces;
using Microsoft.Extensions.Logging;
using OTS.Repositories.Interfaces;

namespace OTS.Services
{
    public class SearchService : ISearchService
    {
        private readonly IAppSettings _config;
        private readonly IUtilService _util;
        private readonly ILookupRepository _lookupRepository;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IAppSettings config, IUtilService util, ILookupRepository lookupRepository, ILogger<SearchService> logger)
        {
            _config = config;
            _util = util;
            _lookupRepository = lookupRepository;
            _logger = logger;
        }

        public async Task<Dictionary<string, dynamic>> GetAsync(string keywords)
        {
            var searchEngines = await _lookupRepository.ListAsync(); // Get the list of search engines from DB

            var response = new Dictionary<string, dynamic>();
            
            if (searchEngines.Any())
            {
                _logger.LogInformation($"Search Started");

                searchEngines.ToList().ForEach(x =>
                {
                    _logger.LogInformation($"Loop through the search engine = {x.Name}");

                    try
                    {
                        var sites = new List<string>();

                        for (var page = 1; page < _config.MaxPages + 1; page++)
                        {
                            _logger.LogInformation($"Search Engine = {x.Name} and Page = {page}");

                            var urls = _util.GetUrls(x.Name, page, _config.SearchPage);

                            sites.AddRange(urls);
                        }

                        if (sites.Any())
                        {
                            var filteredUrls = _util.GetFilteredUrls(sites, _config.SiteName);

                            if (filteredUrls.Any())
                            {
                                response.Add(x.Name, string.Join(",", filteredUrls));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{nameof(SearchService)} {nameof(GetAsync)} {ex.Message}");
                    }
                });

                _logger.LogInformation($"Search Completed");
            }

            return response;
        }
    }
}
