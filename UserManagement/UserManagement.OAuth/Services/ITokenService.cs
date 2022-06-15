using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.OAuth.Services
{
public interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}
}
