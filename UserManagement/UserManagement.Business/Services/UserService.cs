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
    public class UserService : IUserService
    {
        private UserManagementContext context = null;
        private ILogger logger;
        private IConfiguration configuration;
        public UserService(UserManagementContext context,
            ILogger<UserService> logger, IConfiguration configuration)
        {
            this.context = context;
            this.logger = logger;
            this.configuration = configuration;
        }
        public IQueryable<UserVM> GetAllUser()
        {
            try
            {
                this.logger.LogDebug("In UserService.GetAllUser");
                var query = from user in context.Users
                            join uaddress in context.UserAddress
                            on user.AddressId equals uaddress.AddressId
                            select new UserVM()
                            {
                                Id = user.UserId,
                                Name = user.Name,
                                Designation = user.Designation,
                                JoiningDate = user.JoiningDate.ToShortDateString(),
                                Image = user.ImagePath,
                                Country = uaddress.Country,
                                FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                            };
                return query;
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.GetAllUser Exception Message= " + e.Message);
                return new UserVM[] { }.AsQueryable();
            }
            finally
            {
                this.logger.LogDebug("Out UserService.GetAllUser");
            }
        }
        public UserVM GetUserDetails(int Id)
        {
            try
            {
                this.logger.LogDebug("In UserService.GetUserDetails: Id {0}", Id);
                var query = from user in context.Users
                            join uaddress in context.UserAddress
                            on user.AddressId equals uaddress.AddressId
                            where (user.UserId == Id)
                            select new UserVM()
                            {
                                Id = user.UserId,
                                Name = user.Name,
                                Designation = user.Designation,
                                JoiningDate = user.JoiningDate.ToShortDateString(),
                                Image = user.ImagePath,
                                Country = uaddress.Country,
                                FullAddress = uaddress.Street + " " + uaddress.State + " " + uaddress.City + " " + uaddress.PinCode
                            };
                return query.FirstOrDefault();
            }
            catch (Exception e)
            {

                this.logger.LogDebug("In UserService.GetUserDetails Exception Message= " + e.Message);
                return new UserVM();
            }
            finally
            {
                this.logger.LogDebug("Out UserService.GetUserDetails");
            }
        }
    }
}
