
using EduConnect.Api.Filters;
using EduConnect.Core.DTOs;
using EduConnect.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var result = await _studentService.GetAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        // POST: api/student
        [HttpPost]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Add(StudentDto studentDto)
        {
            var result = await _studentService.AddAsync(studentDto);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPost("with-courses")]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> AddWithCourses([FromBody] StudentDto studentDto, [FromQuery] List<int> courseIds)
        {
            var result = await _studentService.AddStudentWithCoursesAsync(studentDto, courseIds);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }


        // PUT: api/student
        [HttpPut]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Update(StudentDto studentDto)
        {
            var result = await _studentService.UpdateAsync(studentDto);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _studentService.DeleteAsync(id);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

    }
}
