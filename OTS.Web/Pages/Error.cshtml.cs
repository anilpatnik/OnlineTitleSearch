using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace OTS.Web.Pages
{
    /// <summary>
    /// 
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_logger"></param>
        public ErrorModel(ILogger<ErrorModel> _logger)
        {
            logger = _logger;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// 
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
