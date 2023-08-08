using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Dto;
using University.Interfaces;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : Controller
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IMapper _mapper;

        public TutorController(ITutorRepository tutorRepository, IMapper mapper)
        {
            _tutorRepository = tutorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tutor>))]
        public IActionResult GetTutors()
        {
            var tutors = _mapper.Map<List<TutorDto>>(_tutorRepository.GetTutors());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(tutors);

        }
        [HttpGet("{tutorId}")]
        [ProducesResponseType(200, Type = typeof(Tutor))]
        [ProducesResponseType(400)]
        public IActionResult GetTutor(int tutorId)
        {
            if (!_tutorRepository.TutorExists(tutorId))
                return NotFound();
            var tutor = _mapper.Map<TutorDto>(_tutorRepository.GetTutor(tutorId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(tutor);

        }
        [HttpGet("/Tutor/Course/{courseId}")]
        [ProducesResponseType(200, Type = typeof(Tutor))]
        [ProducesResponseType(400)]

        public IActionResult GetTutorByCourse(int courseId)
        {
            var tutor = _mapper.Map<TutorDto>(_tutorRepository.GetTutorByCourse(courseId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(tutor);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTutor([FromBody] TutorDto tutorCreate)
        {
            if (tutorCreate == null)
                return BadRequest(ModelState);
            var tutor = _tutorRepository.GetTutors().Where(c => c.TutorName.Trim().ToUpper()
             == tutorCreate.TutorName.TrimEnd().ToUpper()).FirstOrDefault();
            if (tutor != null)
            {
                ModelState.AddModelError("", "Tutor already exists");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tutorMap = _mapper.Map<Tutor>(tutorCreate);


            if (!_tutorRepository.CreateTutor(tutorMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully Created");

        }
        [HttpPut("{tutorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTutor(int tutorId, [FromBody] TutorDto updatedTutor)
        {
            if (updatedTutor == null)
                return BadRequest(ModelState);
            if (tutorId != updatedTutor.TutorId)
                return BadRequest(ModelState);
            if (!_tutorRepository.TutorExists(tutorId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            var tutorMap = _mapper.Map<Tutor>(updatedTutor);
            if (!_tutorRepository.UpdateTutor(tutorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating tutor");
                return StatusCode(500, ModelState);

            }
            return NoContent();

        }
        [HttpDelete("{tutorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTutor(int tutorId)
        {
            if (!_tutorRepository.TutorExists(tutorId))
                return NotFound();
            var tutorToDelete = _tutorRepository.GetTutor(tutorId);
            if (!ModelState.IsValid)
                return BadRequest();
            if (!_tutorRepository.DeleteTutor(tutorToDelete))

                ModelState.AddModelError("", "Something went wrong while deleting tutor");
            return NoContent();


        }
    }
    }
