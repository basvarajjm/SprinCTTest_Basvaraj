using System.ComponentModel.DataAnnotations;

namespace SprinCTTest_Basvaraj.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }

    public class AssignCoursesToStudentModel
    {

        [Required(ErrorMessage = "Student Id is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Course Id is required")]
        public string CommaSeparatedCourseIds { get; set; }
    }

    public class StudentCoursesModel : StudentModel
    {
        public string CourseEnrolled { get; set; }
    }
}