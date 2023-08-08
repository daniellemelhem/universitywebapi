using University.Models;

namespace University.Interfaces
{
    public interface ITutorRepository
    {
        ICollection<Tutor> GetTutors();
        Tutor GetTutor(int tutorId);
        Tutor GetTutorByCourse(int courseId);
        bool TutorExists(int tutorId);
        bool CreateTutor(Tutor tutor);
        bool UpdateTutor(Tutor tutor);
        bool DeleteTutor(Tutor tutor);
        bool Save();

    }
}
