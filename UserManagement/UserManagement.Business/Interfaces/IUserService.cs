using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Model;
using UserManagement.ViewModel;

namespace UserManagement.Business.Interfaces
{
    public interface IUserService
    {
        IQueryable<UserVM> GetAllUser();
        UserVM GetUserDetails(int Id);

    }
}
