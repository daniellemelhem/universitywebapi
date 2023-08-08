using University.Data;
using University.Interfaces;
using University.Models;

namespace University.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
          _context = context;
        }
        public bool CourseExists(int courseId)
        {
           return _context.Courses.Any(c=>c.CourseId == courseId);  
        }

        public bool CreateCourse(Course course)
        {
            _context.Add(course);
            return Save();
        }

        public bool DeleteCourse(Course course)
        {
            _context.Remove(course);
            return Save();
        }

        public Course GetCourse(int courseId)
        {
            return _context.Courses.Where(c=>c.CourseId==courseId).FirstOrDefault();
        }

        
        public ICollection<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCourse(Course course)
        {
            _context.Update(course);
            return Save();
        }
    }
}
