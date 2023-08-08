using University.Models;

namespace University.Interfaces
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int courseId);
        
        bool CourseExists(int courseId);
        //Create Course
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(Course course);
        bool Save();

    }
}
