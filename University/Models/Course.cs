namespace University.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public Tutor tutor { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }


    }
}
