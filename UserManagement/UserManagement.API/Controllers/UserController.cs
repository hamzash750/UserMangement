using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Business.Interfaces;
using UserManagement.ViewModel;

namespace UserManagement.API.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
 
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet, Route("GetUserList")]
        public IEnumerable<UserVM> GetUserList()
        {
            try
            {
                this._logger.LogDebug("In UserController.GetUserList");
                return _userService.GetAllUser();
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserController.GetUserList Exception Message= " + e.Message);
                return new List<UserVM>();
            }
            finally
            {
                this._logger.LogDebug("Out UserController.GetUserList");
            }

        }
        
        [HttpGet, Route("GetUserDetails")]
        public IActionResult GetUserDetails(int Id)
        {
            try
            {
                this._logger.LogDebug("In UserController.GetUserDetails: Id {0}", Id);
                if (Id > 0)
                {
                    var User = _userService.GetUserDetails(Id);
                    if (User.Id > 0)
                    {
                        return Ok(User);
                    }
                    else
                    {
                        return NotFound("User Not Exist");
                    }
                }
                else
                {
                    return BadRequest("Please give user Id");
                }
                
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserController.GetUserDetails Exception Message= " + e.Message);
                return NotFound("Exception Occured" + e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out UserController.GetUserDetails");
            }

        }

    }
}
