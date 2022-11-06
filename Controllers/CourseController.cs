using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SprinCTTest_Basvaraj.BAL;
using SprinCTTest_Basvaraj.Models;

namespace SprinCTTest_Basvaraj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        public CourseController(ILogger<CourseController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(CourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CourseBAL courseBAL = new CourseBAL(_configuration);
            var result = await courseBAL.AddCourse(model);
            return Ok(result);
        }

        [HttpDelete("DeleteCourse/Id")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            CourseBAL courseBAL = new CourseBAL(_configuration);
            var result = await courseBAL.DeleteCourse(id);
            return Ok(result);
        }
    }
}
