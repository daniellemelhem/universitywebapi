using University.Models;

namespace University.Interfaces
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        ICollection<Student> GetStudentByCourse(int courseId);
        bool StudentExists(int studentId);
        bool CreateStudent(int courseId, Student student);
        bool UpdateStudent(int courseId, Student student);
        bool DeleteStudent(Student student);
        bool Save();
    }
}
