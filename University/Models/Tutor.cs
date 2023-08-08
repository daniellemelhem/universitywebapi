namespace University.Models
{
    public class Tutor
    {
        public int TutorId { get; set; }
        public string TutorName { get; set;}
        public ICollection<Course> Courses { get; set; }

    }
}
