using SprinCTTest_Basvaraj.DAL;
using SprinCTTest_Basvaraj.Models;

namespace SprinCTTest_Basvaraj.BAL
{
    public class StudentBAL
    {
        private readonly IConfiguration _configuration;
        private readonly StudentDAL StudentDAL;

        public StudentBAL(IConfiguration configuration)
        {
            _configuration = configuration;
            StudentDAL = new StudentDAL(_configuration);
        }

        public ResponseModel<StudentModel> AddStudent(StudentModel model)
        {
            return StudentDAL.AddStudent(model);
        }

        public ResponseModel<object> AssignCoursesToStudent(AssignCoursesToStudentModel model)
        {
            return StudentDAL.AssignCoursesToStudent(model);
        }

        public ResponseModel<List<StudentCoursesModel>> GetStudentsAndCourseEnrolledList()
        {
            return StudentDAL.GetStudentsAndCourseEnrolledList();
     
        }
        
        public ResponseModel<List<GetStudentByCourseModel>> GetStudentsListByCourseName(string courseName)
        {
            return StudentDAL.GetStudentsListByCourseName(courseName);

        }
    }
}
