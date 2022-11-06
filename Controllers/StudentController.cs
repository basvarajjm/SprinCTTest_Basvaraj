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
        public IActionResult AddStudent(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = StudentBAL.AddStudent(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("AssignCourse")]
        public IActionResult AssignCourse(AssignCoursesToStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = StudentBAL.AssignCoursesToStudent(model);
            return Ok(result);
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudentsList()
        {
            var result = StudentBAL.GetStudentsAndCourseEnrolledList();
            return Ok(result);
        }

        [HttpGet("GetStudentsListByCourseName")]
        public IActionResult GetStudentsListByCourseName(string courseName)
        {
            var result = StudentBAL.GetStudentsListByCourseName(courseName);
            return Ok(result);
        }

    }
}