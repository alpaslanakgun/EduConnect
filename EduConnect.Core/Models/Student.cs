using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduConnect.Core.Common;

namespace EduConnect.Core.Entities
{
    public class Student: BaseEntity, IEntity
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public ICollection<Course> Courses { get; set; }
    }
}
