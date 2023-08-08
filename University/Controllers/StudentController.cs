using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Interfaces;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudents());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(students);

        }
        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();
            var student = _mapper.Map<StudentDto>(_studentRepository.GetStudent(studentId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(student);

        }
        [HttpGet("/Student/Course/{courseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentByCourse(int courseId)
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepository.GetStudentByCourse(courseId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(students);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromQuery]int  courseId,[FromBody] StudentDto studentCreate)
        {
            if (studentCreate == null)
                return BadRequest(ModelState);
            var student = _studentRepository.GetStudents().Where(c => c.LastName.Trim().ToUpper()
             == studentCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
            if (student != null)
            {
                ModelState.AddModelError("", "Student already exists");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var studentMap = _mapper.Map<Student>(studentCreate);
            //studentMap.tutor = _tutorRepository.GetTutor(tutorId);
            ////studentMap.StudentCourses =
            //for (int i = 0; i < courseIds.Count; i++)
            //{
            //    var x = _courseRepository.GetCourse(courseIds[i]);
            //    studentMap.StudentCourses.Add(x);
            //    i++;
            //}

            if (!_studentRepository.CreateStudent(courseId, studentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully Created");

        }
        [HttpPut("{studentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent([FromQuery] int tutorId, int studentId,
       [FromBody]StudentDto updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest(ModelState);
            if (studentId != updatedStudent.StudentId)
                return BadRequest(ModelState);
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var studentMap = _mapper.Map<Student>(updatedStudent);
            if (!_studentRepository.UpdateStudent(tutorId,studentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating student");
                return StatusCode(500, ModelState);

            }
            return NoContent();


        }
        [HttpDelete("{studentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStudent(int studentId)
        {
            if (!_studentRepository.StudentExists(studentId))
                return NotFound();
            var studentToDelete = _studentRepository.GetStudent(studentId);
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_studentRepository.DeleteStudent(studentToDelete))

                ModelState.AddModelError("", "Something went wrong while deleting student");
            return NoContent();

        }
    }
    }
