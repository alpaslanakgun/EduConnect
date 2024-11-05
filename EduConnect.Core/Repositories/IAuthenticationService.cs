using EduConnect.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Core.Repositories
{
    public interface IAuthenticationService
    {
        bool CreateUser(User userAddDto, string password);
        Task<bool> SignOut();
        User AuthenticateUser(string userName, string password);
        User GetUser(string username);
    }
}
