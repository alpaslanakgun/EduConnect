﻿using EduConnect.Api.Filters;
using EduConnect.Core.DTOs;
using EduConnect.Core.Entities;
using EduConnect.Services.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IValidator<CourseDto> _courseValidator;


        public CourseController(ICourseService courseService, IValidator<CourseDto> courseValidator)
        {
            _courseService = courseService;
            _courseValidator = courseValidator;
        }

        // GET: api/course/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var result = await _courseService.GetAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        // GET: api/course
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseService.GetAllAsync();
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        // POST: api/course
        [HttpPost]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Add(CourseDto courseDto)
        {
            var validationResult = await _courseValidator.ValidateAsync(courseDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _courseService.AddAsync(courseDto);
            return result.Success ? CreatedAtAction(nameof(Get), new { id = result.Data.Id }, result.Data) : BadRequest(result.Message);
        }

        // PUT: api/course
        [HttpPut]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Update(CourseDto courseDto)
        {
            var result = await _courseService.UpdateAsync(courseDto);
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        // DELETE: api/course/{id}
        [HttpDelete("{id}")]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // HARD DELETE: api/course/harddelete/{id}
        [HttpDelete("harddelete/{id}")]
        [RoleAuthorize("Admin")]
        public async Task<IActionResult> HardDelete(int id)
        {
            var result = await _courseService.HardDeleteAsync(id);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }
    }
}
