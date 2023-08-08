using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Tutor> Tutors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>()
            .HasOne(s => s.Student)
            .WithMany(sc => sc.StudentCourses)
            .HasForeignKey(s => s.StudentId);
            modelBuilder.Entity<StudentCourse>()
               .HasOne(c => c.Course)
               .WithMany(sc => sc.StudentCourses)
               .HasForeignKey(c => c.CourseId);
        }
    }
}
