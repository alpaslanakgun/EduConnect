using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace EduConnect.Core.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }



        [NotMapped]
        public string[] Roles { get; set; }
    }
}

