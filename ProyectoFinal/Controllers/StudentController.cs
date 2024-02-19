using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Services;
using static ProyectoFinal.DTOs.StudentDTO;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;
        private readonly IValidationsManager _validationsManager;

        public StudentController(IStudentService studentService, IValidationsManager validationsManager)
        {
            _studentService = studentService;
            _validationsManager = validationsManager;

        }

        //[Authorize(Roles = "Estudiante")]
        [HttpPut("EstudianteInscribirCarreras")]
        public async Task<IActionResult> EnrollCareerStudent(EnrollCareerStudentRequestDto student)
        {
            var validation = await _validationsManager.ValidateAsync(student);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var studentExists = await _validationsManager.ValidateStudentExistAsync(student.EstudianteId);

            if (!studentExists)
            {
                return BadRequest("El estudiante no existe.");
            }

            try
            {
                await _studentService.EnrollCareerStudent(student);
                return Ok("El estudiante se ha matriculado en la carrera exitosamente.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        //[Authorize(Roles = "Estudiante")]
        [HttpPost("EstudianteInscribirMaterias")]
        public async Task<IActionResult> EnrollSubjectStudent(EnrollSubjectStudentRequestDto student)
        {
            var validation = await _validationsManager.ValidateAsync(student);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var studentExists = await _validationsManager.ValidateStudentExistAsync(student.EstudianteId);

            if (!studentExists)
            {
                return BadRequest("El estudiante no existe.");
            }

            try
            {
                await _studentService.EnrollSubjectStudent(student);
                return Ok("El estudiante se ha inscrito a la materia exitosamente.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }

        //[Authorize(Roles = "Estudiante")]
        [HttpGet("EstudianteMostrarTodasLasMaterias")]
        public async Task<IActionResult> AllSubjectsTaught([FromQuery] AllSubjectsStudentRequestDto student)
        {
            var validation = await _validationsManager.ValidateAsync(student);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var studentExists = await _validationsManager.ValidateStudentExistAsync(student.EstudianteId);

            if (!studentExists)
            {
                return BadRequest("El estudiante no existe.");
            }

            try
            {
                var lista = await _studentService.SubjectsEnrolledByStudent(student);
                return Ok(lista);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }


        //[Authorize(Roles = "Estudiante")]
        [HttpGet("EstudianteMostrarTodasTareas")]
        public async Task<IActionResult> GetAllAssignmentsForStudent([FromQuery] AllSubjectsStudentRequestDto student)
        {
            var validation = await _validationsManager.ValidateAsync(student);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var studentExists = await _validationsManager.ValidateStudentExistAsync(student.EstudianteId);

            if (!studentExists)
            {
                return BadRequest("El estudiante no existe.");
            }

            try
            {
                var lista = await _studentService.GetAllAssignmentsForStudent(student);
                return Ok(lista);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }
    }
}
