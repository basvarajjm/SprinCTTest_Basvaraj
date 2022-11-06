using Microsoft.AspNetCore.Mvc;
using SprinCTTest_Basvaraj.BAL;
using SprinCTTest_Basvaraj.Models;
using System.Reflection;

namespace SprinCTTest_Basvaraj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly StudentBAL StudentBAL;

        public StudentController(ILogger<StudentController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            StudentBAL = new StudentBAL(_configuration);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await StudentBAL.AddStudent(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("AssignCourse")]
        public async Task<IActionResult> AssignCourse(AssignCoursesToStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await StudentBAL.AssignCoursesToStudent(model);
            return Ok(result);
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudentsList()
        {
            var result = await StudentBAL.GetStudentsAndCourseEnrolledList();
            return Ok(result);
        }

        [HttpGet("GetStudentsListByCourseName")]
        public async Task<IActionResult> GetStudentsListByCourseName(string courseName)
        {
            var result = await StudentBAL.GetStudentsListByCourseName(courseName);
            return Ok(result);
        }

    }
}