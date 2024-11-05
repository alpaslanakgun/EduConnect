using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Core.DTOs
{
    public class StudentDto:BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
