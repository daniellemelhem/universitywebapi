using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Interfaces;
using University.Models;

namespace University.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository,
            ITutorRepository tutorRepository
            , IMapper mapper)
        {
            _courseRepository = courseRepository;
           _tutorRepository = tutorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetCourses());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(courses);

        }
        [HttpGet("{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();
            var course = _mapper.Map<CourseDto>(_courseRepository.GetCourse(courseId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(course);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromQuery] int tutorId ,[FromBody] CourseDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);
            var course = _courseRepository.GetCourses().Where(c => c.CourseName.Trim().ToUpper()
             ==courseCreate.CourseName.TrimEnd().ToUpper()).FirstOrDefault();
            if (course != null)
            {
                ModelState.AddModelError("", "Course already exists");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var courseMap = _mapper.Map<Course>(courseCreate);
            courseMap.tutor = _tutorRepository.GetTutor(tutorId);

            if (!_courseRepository.CreateCourse(courseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully Created");

        }
        [HttpPut("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int courseId, [FromBody] CourseDto updatedCourse)
        {
            if (updatedCourse == null)
                return BadRequest(ModelState);
            if (courseId != updatedCourse.CourseId)
                return BadRequest(ModelState);
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var courseMap = _mapper.Map<Course>(updatedCourse);
            if(!_courseRepository.UpdateCourse(courseMap))
            {
                ModelState.AddModelError("", "Something went wrong updating course");
                return StatusCode(500,ModelState);

            }
            return NoContent();

        }
        [HttpDelete("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();
            var courseToDelete=_courseRepository.GetCourse(courseId);
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_courseRepository.DeleteCourse(courseToDelete))
            
                ModelState.AddModelError("", "Something went wrong while deleting course");
            return NoContent();

            
        }

    }
}
