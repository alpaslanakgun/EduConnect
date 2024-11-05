using EduConnect.Core.DTOs;
using EduConnect.Core.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Services.Abstract
{
    public interface IStudentService
    {
        Task<IDataResult<StudentDto>> GetAsync(int studentId);              // Belirli bir öğrenci bilgisi getir
        Task<IDataResult<List<StudentDto>>> GetAllAsync();                  // Tüm öğrencileri getir
        Task<IDataResult<StudentDto>> AddAsync(StudentDto studentDto);      // Yeni bir öğrenci ekle
        Task<IDataResult<StudentDto>> UpdateAsync(StudentDto studentDto);   // Öğrenci bilgilerini güncelle
        Task<IDataResult<StudentDto>> DeleteAsync(int studentId);           // Öğrenciyi soft delete yap
        Task<IResult> HardDeleteAsync(int studentId);
    }
}
