using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interfaces;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IValidationsManager _validationsManager;

        public TeacherController(ITeacherService teacherService, IValidationsManager validationsManager)
        {
            _teacherService = teacherService;
            _validationsManager = validationsManager;

        }

        //[Authorize(Roles = "Profesor")]
        [HttpPost("ProfesorAsignarMaterias")]
        public async Task<IActionResult> TeachMatterSubject(TeachMatterRequestDto teacher)
        {
            var validation = await _validationsManager.ValidateAsync(teacher);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var teacherExists = await _validationsManager.ValidateTeacherExistAsync(teacher.ProfesorId);

            if (!teacherExists)
            {
                return BadRequest("El profesor no existe.");
            }

            try
            {
                await _teacherService.TeachMatterSubject(teacher);
                return Ok("El profesor va impartir la materia exitosamente.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }

        //[Authorize(Roles = "Profesor")]
        [HttpGet("ProfesorMostrarMaterias")] 
        public async Task<IActionResult> AllSubjectsTaught([FromQuery] AllSubjectsTaughtRequestDto teacher)
        {
            var validation = await _validationsManager.ValidateAsync(teacher);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var teacherExists = await _validationsManager.ValidateTeacherExistAsync(teacher.ProfesorId);

            if (!teacherExists)
            {
                return BadRequest("El profesor no existe.");
            }

            try
            {
                var lista = await _teacherService.AllSubjectsTaught(teacher);
                return Ok(lista);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }

        //[Authorize(Roles = "Profesor")]
        [HttpPost("CrearTarea")]
        public async Task<IActionResult> CreateTask(TaskPublishRequestDto task)
        {
            var validation = await _validationsManager.ValidateAsync(task);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var teacherExists = await _validationsManager.ValidateTeacherExistAsync(task.ProfesorId);

            if (!teacherExists)
            {
                return BadRequest("El profesor no existe.");
            }

            var subjectExists = await _validationsManager.ValidateTeacherSubjectExistAsync(task.ProfesorId, task.MateriaId);

            if (!subjectExists)
            {
                return BadRequest("El profesor no tiene asignada esta materia o no existe.");
            }

            try
            {
                await _teacherService.CreateTask(task);
                return Ok("Se ha publicado la tarea exitosamente.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.InnerException);
            }
        }
    }
}
