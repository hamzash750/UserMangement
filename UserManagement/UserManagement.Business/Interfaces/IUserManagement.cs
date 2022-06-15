using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.ViewModel;

namespace UserManagement.Business.Interfaces
{
    public interface IUserManagement
    {
        UserVM DeleteUser(int Id);
        UserVM InsertUer(UserAddressVM Uvm);
        int InsertUserAddress(AddressVM UAddress);
        UserVM UpdateUser(UserUpdateAddressVM Uvm);
        int UpdateUserAddress(AddressVM UAddress, int Id);
         Task<bool> UpdateUserImageAsync(FileUploadVM ufile, string webRootPath);
    }
}
