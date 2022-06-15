using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UserManagement.Business.Interfaces;
using UserManagement.ViewModel;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SearchingController : ControllerBase
    {
        private readonly ILogger<SearchingController> _logger;
        private readonly ISearch _searchService;

        public SearchingController(
            ILogger<SearchingController> logger, ISearch searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }
        [HttpPost, Route("SearchUser")]
        public IActionResult SearchSubmittedAlerts(SearchVM query)
        {
            try
            {
                this._logger.LogDebug("In SearchingController.SearchUser");
                
                return Ok(_searchService.GetSearch(query));
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In SearchingController.SearchUser Exception Message= " + e.Message);
                return NotFound(e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out SearchingController.SearchUser");
            }

        }
    }
}
