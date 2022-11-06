using System.ComponentModel.DataAnnotations;

namespace SprinCTTest_Basvaraj.Models
{
    public class CourseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Professor Name is required")]
        public string ProfessorName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }

    public class GetStudentByCourseModel
    {
        public string CourseName { get; set; }
        public List<StudentModel> Students { get; set; }
    }
}
