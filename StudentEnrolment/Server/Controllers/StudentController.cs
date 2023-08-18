using Microsoft.AspNetCore.Mvc;
using StudentEnrolment.Server.Interfaces;
using StudentEnrolment.Shared;
using StudentEnrolment.Shared.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrolment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentService.GetStudents();
        }

        [HttpGet("SearchStudents")]
        public IEnumerable<Student> SearchStudents(string? id)
        {
            id = (id == null) ? "" : id;
            if(int.TryParse(id, out _))
            {
                return _studentService.SearchStudentsById(int.Parse(id));
            }
            else
            {
                return _studentService.SearchStudentsByName(id);
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public AddStudentViewModel Get(int id)
        {
            return _studentService.GetStudentById(id);
        }

        [HttpGet("GetStudentForUpdate")]
        public UpdateStudentViewModel GetStudentForUpdate(int id)
        {
            return _studentService.GetStudentForUpdate(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] AddStudentViewModel StudentVM)
        {
            _studentService.AddStudent(StudentVM);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] UpdateStudentViewModel student, int id)
        {
            _studentService.UpdateStudent(student, id);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _studentService.DeleteStudent(id);
        }
    }
}
