using EduConnect.Core.Configuration;
using EduConnect.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserDto userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
