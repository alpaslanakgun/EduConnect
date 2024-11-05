using AutoMapper;
using EduConnect.Core.DTOs;
using EduConnect.Core.Entities;
using EduConnect.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
    
        }

    }
}
