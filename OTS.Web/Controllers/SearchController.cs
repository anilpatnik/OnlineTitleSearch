using System.Collections.Generic;
using System.Threading.Tasks;
using OTS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OTS.Web.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// Return a string of numbers for where the URL is found in the search engine’s results
        /// </summary>
        /// <param name="keywords">String of search keywords</param>
        /// <returns>String of numbers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<Dictionary<string, dynamic>> GetAsync(string keywords)
        {
            return await _searchService.GetAsync(keywords);
        }
    }
}
