using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OTS.Models;
using OTS.Services.Common;
using OTS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OTS.Web.Util;

namespace OTS.Web.Controllers
{
    // [Authorize] // TODO: required for submit calls
    public class LookupController : BaseController
    {
        private readonly ILookupService _lookupService;
        private readonly ILogger<LookupController> _logger;

        public LookupController(ILookupService lookupService, ILogger<LookupController> logger)
        {
            _lookupService = lookupService;
            _logger = logger;
        }
        
        private async Task<IActionResult> GetResponse<T>(Func<Task<Response<T>>> func)
        {
            var response = await Task.Run(func);

            if (response.Success) return Ok(response.Resource);
            
            _logger.LogInformation(response.Message);
                
            return BadRequest(new Error(response.Message));
        }

        /// <summary>
        /// Lists all search engines
        /// </summary>
        /// <returns>List all search engines</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Lookup>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _lookupService.ListAsync());
        }

        /// <summary>
        /// Returns search engine by identifier
        /// </summary>
        /// <param name="id">Search engine identifier</param>
        /// <returns>Response for the request</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Lookup), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            return Ok(await _lookupService.FindByIdAsync(id));
        }

        /// <summary>
        /// Saves a new search engine
        /// </summary>
        /// <param name="model">New search engine data</param>
        /// <returns>Response for the request</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Lookup), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody] Lookup model)
        {
            return await GetResponse(() => _lookupService.SaveAsync(model));
        }

        /// <summary>
        /// Updates an existing search engine by identifier
        /// </summary>
        /// <param name="id">Search engine identifier</param>
        /// <param name="model">Updated search engine data</param>
        /// <returns>Response for the request</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Lookup), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Lookup model)
        {
            return await GetResponse(() => _lookupService.UpdateAsync(id, model));
        }

        /// <summary>
        /// Deletes search engine by identifier
        /// </summary>
        /// <param name="id">Search engine identifier</param>
        /// <returns>Response for the request</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Lookup), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await GetResponse(() => _lookupService.DeleteAsync(id));
        }
    }
}
