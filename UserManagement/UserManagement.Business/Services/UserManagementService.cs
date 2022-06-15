using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Business.Interfaces;
using UserManagement.Model;
using UserManagement.Repository;
using UserManagement.ViewModel;

namespace UserManagement.Business.Services
{
    public class UserManagementService : IUserManagement
    {
        private UserManagementContext context = null;
        private ILogger logger;
        private IConfiguration configuration;
        private readonly IUserService _userService;
        private readonly IFileUpload _fileUploadService;
        public UserManagementService(UserManagementContext context,
            ILogger<UserManagementService> logger, IConfiguration configuration, IUserService userService, IFileUpload fileuploadService)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
            _userService = userService;
            _fileUploadService = fileuploadService;
        }
        public  UserVM InsertUer(UserAddressVM Uvm)
        {
            try
            {
                this.logger.LogDebug("In UserService.InsertUer");
                var user = new User();
                user.Name = Uvm.Name;
                user.Designation = Uvm.Designation;
                user.JoiningDate = Uvm.JoiningDate;
                user.AddressId = InsertUserAddress(Uvm.Address);
                var InsertUser = context.Users.Add(user);
                context.SaveChanges();
                return _userService.GetUserDetails(user.UserId);
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.InsertUer Exception Message= " + e.Message);
                return new UserVM();
            }
            finally
            {
                this.logger.LogDebug("Out UserService.InsertUer");
            }
        }
        public int InsertUserAddress(AddressVM ua)
        {
            try
            {
                this.logger.LogDebug("In UserService.InsertUserAddress");
                var UAddress = new UserAddress();
                UAddress.City = ua.City;
                UAddress.Country = ua.Country;
                UAddress.PinCode = ua.PinCode;
                UAddress.State = ua.State;
                UAddress.Street = ua.Street;
                var AddNewAddress = context.Add(UAddress);
                context.SaveChanges();
                return UAddress.AddressId;
            }
            catch (Exception ex)
            {

                this.logger.LogDebug("In UserService.InsertUserAddress Exception Message= " + ex.Message);
                return 0;
            }
        }
        public UserVM UpdateUser(UserUpdateAddressVM Uvm)
        {
            try
            {
                this.logger.LogDebug("In UserService.UpdateUser: Id {0}");
                var user = context.Users.FirstOrDefault(x => x.UserId == Uvm.UId);
                if (user != null)
                {
                    user.Name = Uvm.Name;
                    user.Designation = Uvm.Designation;
                    user.JoiningDate = Uvm.JoiningDate;
                    user.ImagePath = Uvm.ImagePath;
                    user.AddressId = UpdateUserAddress(Uvm.Address, user.AddressId);
                    context.SaveChanges();
                    return _userService.GetUserDetails(user.UserId);
                }
                else
                {
                    return new UserVM();
                }

            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.UpdateUser Exception Message= " + e.Message);
                return new UserVM();
            }
            finally
            {
                this.logger.LogDebug("Out UserService.UpdateUser");
            }
        }
        public int UpdateUserAddress(AddressVM ua, int AddressId)
        {
            try
            {
                this.logger.LogDebug("In UserService.InsertUserAddress");
                var UAddress = context.UserAddress.FirstOrDefault(x => x.AddressId == AddressId);
                if (UAddress != null)
                {
                    UAddress.City = ua.City;
                    UAddress.Country = ua.Country;
                    UAddress.PinCode = ua.PinCode;
                    UAddress.State = ua.State;
                    UAddress.Street = ua.Street;
                    context.SaveChanges();
                    return UAddress.AddressId;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {

                this.logger.LogDebug("In UserService.InsertUserAddress Exception Message= " + ex.Message);
                return 0;
            }
        }
        public UserVM DeleteUser(int Id)
        {
            try
            {
                this.logger.LogDebug("In UserService.DeleteUser: Id {0}", Id);
                var query = from user in context.Users
                            join uaddress in context.UserAddress
                            on user.AddressId equals uaddress.AddressId
                            where (user.UserId == Id)
                            select new
                            {
                                UserAddress = uaddress,
                                Users = user,
                                userVM = new UserVM()
                                {
                                    Id = user.UserId,
                                    Name = user.Name,
                                    Designation = user.Designation,
                                    JoiningDate = user.JoiningDate.ToShortDateString(),
                                    Image = user.ImagePath,
                                    Country = uaddress.Country,
                                    FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                                }
                            };
                var currentUser = query.FirstOrDefault().userVM;
                foreach (var item in query)
                {
                    context.UserAddress.Attach(item.UserAddress);
                    context.UserAddress.Remove(item.UserAddress);
                    context.Users.Attach(item.Users);
                    context.Users.Remove(item.Users);
                    context.SaveChanges();
                }
                return currentUser;
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.DeleteUser Exception Message= " + e.Message);
                return new UserVM();
            }
            finally
            {
                this.logger.LogDebug("Out UserService.DeleteUser");
            }
        }
        public async Task<bool> UpdateUserImageAsync(FileUploadVM Uvm, string path)
        {
            try
            {
                this.logger.LogDebug("In UserService.UpdateUserImage: Id {0}");
                var user = context.Users.FirstOrDefault(x => x.UserId == Uvm.UId);
                if (user != null)
                {
                    user.ImagePath =await _fileUploadService.Upload(Uvm.UserImage, path);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.UpdateUser Exception Message= " + e.Message);
                return false;
            }
            finally
            {
                this.logger.LogDebug("Out UserService.UpdateUser");
            }
        }

    }
}
