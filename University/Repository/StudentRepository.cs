using University.Data;
using University.Interfaces;
using University.Models;

namespace University.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;
        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Student> GetStudentByCourse(int courseId)
        {
           
            return _context.StudentCourses.Where(c => c.CourseId == courseId).Select(c => c.Student).ToList();
        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Where(s => s.StudentId == id).FirstOrDefault();
        }

        public bool StudentExists(int studentId)
        {
            return _context.Students.Any(s => s.StudentId == studentId);
        }

        public bool CreateStudent(int courseId, Student student)

        {
            var course = _context.Courses.Where(a => a.CourseId == courseId).FirstOrDefault();
            var StudentCourse = new StudentCourse()
            {
                Student = student,
                Course = course,
            };
            _context.Add(StudentCourse);
            _context.Add(student);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateStudent(int courseId, Student student)
        {
            _context.Update(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
           _context.Remove(student);
            return Save();
        }
    }
}
