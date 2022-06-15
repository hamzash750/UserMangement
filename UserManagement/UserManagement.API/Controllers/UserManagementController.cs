using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagement.Business.Interfaces;
using UserManagement.ViewModel;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserManagementController : ControllerBase
    {
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserManagement _userManagementService;
        private readonly IUserService _userService;
        private static IWebHostEnvironment _webHostEnvironment;
        public UserManagementController(
            ILogger<UserManagementController> logger, IUserManagement userManagementService, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userManagementService = userManagementService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost, Route("InsertNewUser")]
        public  IActionResult InsertNewUser(UserAddressVM Ua)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._logger.LogDebug("In UserManagementController.UserAddressVM: Id {0}");
                    var user = _userManagementService.InsertUer(Ua);
                    if (user.Id > 0)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest(Ua);
                }
                
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserManagementController.UserAddressVM Exception Message= " + e.Message);
                return NotFound("Exception Occured"+e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out UserManagementController.UserAddressVM");
            }

        }
        [HttpPut, Route("UpdateUser")]
        public IActionResult UpdateUser(UserUpdateAddressVM Ua)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._logger.LogDebug("In UserManagementController.UpdateUser: Id {0}");
                    var user = _userManagementService.UpdateUser(Ua);
                    if (user.Id > 0)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest("User Not Found");
                    }
                    
                }
                else
                {
                    return BadRequest(Ua);
                }
                
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserManagementController.UpdateUser Exception Message= " + e.Message);
                return NotFound("Exception Occured" + e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out UserManagementController.UpdateUser");
            }

        }

        [HttpDelete, Route("DeleteUser")]
        public IActionResult DeleteUser(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    this._logger.LogDebug("In UserManagementController.DeleteUser: Id {0}", Id);
                    var user = _userManagementService.DeleteUser(Id);
                    if (user.Id > 0)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest("User Not Found");
                    }
                }
                else
                {
                    return BadRequest("Please Give User Id");
                }
                
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserManagementController.DeleteUser Exception Message= " + e.Message);
                return NotFound("Exception Occured" + e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out UserManagementController.DeleteUser");
            }

        }
        [HttpPut, Route("UploadUserImage")]
        public IActionResult UploadUserImage([FromForm] FileUploadVM ufile)
        {
            try
            {
                if (ufile.UId > 0)
                {
                    var user = _userService.GetUserDetails(ufile.UId);
                    if (user.Id > 0)
                    {
                        this._logger.LogDebug("In UserManagementController.UploadUserImage: UserId {0}", ufile.UId);
                        _userManagementService.UpdateUserImageAsync(ufile, _webHostEnvironment.WebRootPath);
                        return Ok(user);
                    }
                    else
                    {
                        return BadRequest("User Not Found");
                    }
                }
                else
                {
                    return BadRequest("Please Give User Id");
                }
              
             

                
            }
            catch (Exception e)
            {
                this._logger.LogDebug("In UserManagementController.UploadUserImage Exception Message= " + e);
                return NotFound("Exception Occured" + e.Message);
            }
            finally
            {
                this._logger.LogDebug("Out UserManagementController.UploadUserImage");
            }

        }


    }
}
