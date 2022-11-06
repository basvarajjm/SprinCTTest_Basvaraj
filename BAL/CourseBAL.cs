using SprinCTTest_Basvaraj.DAL;
using SprinCTTest_Basvaraj.Models;

namespace SprinCTTest_Basvaraj.BAL
{
    public class CourseBAL
    {
        private readonly IConfiguration _configuration;

        public CourseBAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResponseModel<CourseModel> AddCourse(CourseModel model)
        {
            var courseDAL = new CourseDAL(_configuration);
            return courseDAL.AddCourse(model);
        }

        public ResponseModel<CourseModel> DeleteCourse(int id)
        {
            var courseDAL = new CourseDAL(_configuration);
            return courseDAL.DeleteCourse(id);
        }
    }
}
