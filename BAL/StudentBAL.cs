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

        public async Task<ResponseModel<StudentModel>> AddStudent(StudentModel model)
        {
            return await StudentDAL.AddStudent(model);
        }

        public async Task<ResponseModel<object>> AssignCoursesToStudent(AssignCoursesToStudentModel model)
        {
            return await StudentDAL.AssignCoursesToStudent(model);
        }

        public async Task<ResponseModel<List<StudentCoursesModel>>> GetStudentsAndCourseEnrolledList()
        {
            return await StudentDAL.GetStudentsAndCourseEnrolledList();
     
        }
        
        public async Task<ResponseModel<List<GetStudentByCourseModel>>> GetStudentsListByCourseName(string courseName)
        {
            return await StudentDAL.GetStudentsListByCourseName(courseName);

        }
    }
}
