using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Core.DTOs
{
    public class CourseDto:BaseDto
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }       
        public string Instructor { get; set; }    
        public int? Capacity { get; set; }         

    }
}
