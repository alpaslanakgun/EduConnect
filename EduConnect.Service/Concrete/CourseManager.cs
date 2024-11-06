using AutoMapper;
using EduConnect.Core.DTOs;
using EduConnect.Core.Entities;
using EduConnect.Core.Repositories;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.Concrete;
using EduConnect.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using EduConnect.Core.Results.ComplexType;
using EduConnect.Core.UnitOfWorks;
using EduConnect.Services.Common;

namespace EduConnect.Services.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourseManager(ICourseRepository courseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CourseDto>> GetAsync(int courseId)
        {
            var course = await _courseRepository.GetAsync(c => c.Id == courseId && !c.IsDeleted);
            if (course != null)
            {
                var courseDto = _mapper.Map<CourseDto>(course);
                return new SuccessDataResult<CourseDto>(courseDto);
            }
            return new ErrorDataResult<CourseDto>(CourseMessageConstant.NotFound);
        }


        public async Task<IDataResult<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync(c => !c.IsDeleted);
            var courseDtos = _mapper.Map<List<CourseDto>>(courses);

            if (courseDtos.Count == 0)
                return new ErrorDataResult<List<CourseDto>>(CourseMessageConstant.NoCoursesFound);

            return new SuccessDataResult<List<CourseDto>>(courseDtos);
        }



        public async Task<IDataResult<CourseDto>> AddAsync(CourseDto courseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                course.CreatedDate = DateTime.Now; // Oluşturulma tarihi
                course.IsActive = true; // Varsayılan olarak aktif
                await _courseRepository.AddAsync(course);
                await _unitOfWork.CommitAsync();

                var resultDto = _mapper.Map<CourseDto>(course);
                return new SuccessDataResult<CourseDto>(resultDto, CourseMessageConstant.AddSuccessful);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CourseDto>(ex.Message);
            }
        }



        public async Task<IDataResult<CourseDto>> UpdateAsync(CourseDto courseDto)
        {
            var existingCourse = await _courseRepository.GetAsync(c => c.Id == courseDto.Id);

            if (existingCourse == null)
                return new ErrorDataResult<CourseDto>(CourseMessageConstant.NotFound);

            existingCourse = _mapper.Map(courseDto, existingCourse);
            existingCourse.UpdatedDate = DateTime.Now; // Güncellenme tarihi
            await _courseRepository.UpdateAsync(existingCourse);
            await _unitOfWork.CommitAsync();

            var updatedDto = _mapper.Map<CourseDto>(existingCourse);
            return new SuccessDataResult<CourseDto>(updatedDto, CourseMessageConstant.UpdateSuccessful);
        }

        public async Task<IDataResult<CourseDto>> DeleteAsync(int courseId)
        {
            var course = await _courseRepository.GetAsync(c => c.Id == courseId);

            if (course == null || course.IsDeleted)
                return new ErrorDataResult<CourseDto>(CourseMessageConstant.AlreadyDeleted);

            course.IsDeleted = true; // Soft delete
            course.UpdatedDate = DateTime.UtcNow; // Güncellenme tarihi
            await _courseRepository.UpdateAsync(course);
            await _unitOfWork.CommitAsync();

            var resultDto = _mapper.Map<CourseDto>(course);
            return new SuccessDataResult<CourseDto>(resultDto, CourseMessageConstant.DeletionSuccessful);
        }

        public async Task<IResult> HardDeleteAsync(int courseId)
        {
            var course = await _courseRepository.GetAsync(c => c.Id == courseId);

            if (course == null)
                return new ErrorResult(CourseMessageConstant.NotFound);

            await _courseRepository.DeleteAsync(course);
            await _unitOfWork.CommitAsync();

            return new SuccessResult(CourseMessageConstant.HardDeleteSuccessful);
        }
    }
}
