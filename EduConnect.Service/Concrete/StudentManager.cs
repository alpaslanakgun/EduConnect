using AutoMapper;
using EduConnect.Core.DTOs;
using EduConnect.Core.Entities;
using EduConnect.Core.Repositories;
using EduConnect.Core.Results.Abstract;
using EduConnect.Core.Results.ComplexType;
using EduConnect.Core.Results.Concrete;
using EduConnect.Core.UnitOfWorks;
using EduConnect.Services.Abstract;
using EduConnect.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduConnect.Services.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentManager(IStudentRepository studentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<StudentDto>> AddAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddAsync(student);
            await _unitOfWork.CommitAsync();

            var studentResultDto = _mapper.Map<StudentDto>(student);
            return new SuccessDataResult<StudentDto>(studentResultDto, StudentMessageConstant.AddSuccessful);
        }

        public async Task<IDataResult<StudentDto>> DeleteAsync(int studentId)
        {
            var student = await _studentRepository.GetAsync(s => s.Id == studentId);

            if (student == null || student.IsDeleted)
                return new ErrorDataResult<StudentDto>(StudentMessageConstant.AlreadyDeleted);

            student.IsDeleted = true;
            await _studentRepository.UpdateAsync(student);
            await _unitOfWork.CommitAsync();

            var studentResultDto = _mapper.Map<StudentDto>(student);
            return new SuccessDataResult<StudentDto>(studentResultDto, StudentMessageConstant.DeletionSuccessful);
        }

        public async Task<IDataResult<List<StudentDto>>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync(s => !s.IsDeleted);
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            if (studentDtos.Count == 0)
                return new ErrorDataResult<List<StudentDto>>(StudentMessageConstant.NoStudentsFound);

            return new SuccessDataResult<List<StudentDto>>(studentDtos);
        }

        public async Task<IDataResult<StudentDto>> GetAsync(int studentId)
        {
            var student = await _studentRepository.GetAsync(s => s.Id == studentId && !s.IsDeleted);

            if (student == null)
                return new ErrorDataResult<StudentDto>(StudentMessageConstant.NotFound);

            var studentDto = _mapper.Map<StudentDto>(student);
            return new SuccessDataResult<StudentDto>(studentDto);
        }

        public async Task<IResult> HardDeleteAsync(int studentId)
        {
            var student = await _studentRepository.GetAsync(s => s.Id == studentId);

            if (student == null)
                return new ErrorResult(StudentMessageConstant.NotFound);

            await _studentRepository.DeleteAsync(student);
            await _unitOfWork.CommitAsync();

            return new SuccessResult(StudentMessageConstant.HardDeleteSuccessful);
        }

        public async Task<IDataResult<StudentDto>> UpdateAsync(StudentDto studentDto)
        {
            var student = await _studentRepository.GetAsync(s => s.Id == studentDto.Id);

            if (student == null)
                return new ErrorDataResult<StudentDto>(StudentMessageConstant.NotFound);

            student = _mapper.Map(studentDto, student);
            await _studentRepository.UpdateAsync(student);
            await _unitOfWork.CommitAsync();

            var updatedStudentDto = _mapper.Map<StudentDto>(student);
            return new SuccessDataResult<StudentDto>(updatedStudentDto, StudentMessageConstant.UpdateSuccessful);
        }
    }
}
