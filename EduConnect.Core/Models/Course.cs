using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduConnect.Core.Common;

namespace EduConnect.Core.Entities
{
    public  class Course: BaseEntity, IEntity
    {

        public string CourseName { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }         // Süre (örneğin, saat cinsinden)
        public string Instructor { get; set; }     // Eğitmen adı
        public int? Capacity { get; set; }         // Kurs kapasitesi
        public ICollection<Student> Students { get; set; }
    }
}
