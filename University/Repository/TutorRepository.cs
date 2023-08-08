using University.Data;
using University.Interfaces;
using University.Models;

namespace University.Repository
{
    public class TutorRepository : ITutorRepository

    {
        private readonly DataContext _context;

        public TutorRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTutor(Tutor tutor)
        {
            _context.Add(tutor);
            return Save();
        }

        public bool DeleteTutor(Tutor tutor)
        {
            _context.Remove(tutor);
            return Save();
        }

        public Tutor GetTutor(int tutorId)
        {
            return _context.Tutors.Where(t => t.TutorId == tutorId).FirstOrDefault();
        }

        public Tutor GetTutorByCourse(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Select(c => c.tutor).FirstOrDefault();
        }

        public ICollection<Tutor> GetTutors()
        {
            return _context.Tutors.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TutorExists(int tutorId)
        {
            return _context.Tutors.Any(t => t.TutorId == tutorId);
        }

        public bool UpdateTutor(Tutor tutor)
        {
            _context.Update(tutor);
            return Save();
        }
    }
}
