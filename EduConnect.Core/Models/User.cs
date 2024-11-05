using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace EduConnect.Core.Models
{
    public class User : IdentityUser
    {
  
        public string UserName { get; set; }
        public string Email { get; set; }


    }
}

