using EduConnect.Core.DTOs;
using EduConnect.Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Abstract
{
    public interface ICourseService
    {
        Task<IDataResult<CourseDto>> GetAsync(int courseId);
        Task<IDataResult<List<CourseDto>>> GetAllAsync();
        Task<IDataResult<CourseDto>> AddAsync(CourseDto courseDto);

        Task<IDataResult<CourseDto>> UpdateAsync(CourseDto courseDto);
        Task<IDataResult<CourseDto>> DeleteAsync(int courseDto);
        Task<IResult> HardDeleteAsync(int courseId);
    }
}
