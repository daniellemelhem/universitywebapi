using AutoMapper;
using University.Dto;
using University.Models;

namespace University.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();
            CreateMap<Tutor, TutorDto>();
            CreateMap<TutorDto,Tutor>();
        }
    }
}
