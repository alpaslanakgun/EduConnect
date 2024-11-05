using EduConnect.Core.DTOs;
using EduConnect.Core.Models;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<IDataResult<NoDataDto>> RevokeRefreshToken(string refreshToken);
        Task<IDataResult<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
