using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interfaces;
using ProyectoFinal.Models;
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
        [HttpPost("AssignTeacherSubjects")]
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
                return BadRequest("The teacher does not exist.");
            }

            try
            {
                await _teacherService.TeachMatterSubject(teacher);
                return Ok("The teacher will teach the subject successfully.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.InnerException);
            }
        }

        //[Authorize(Roles = "Profesor")]
        [HttpGet("ShowTeacherSubjects")]
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
                return BadRequest("The teacher does not exist.");
            }

            try
            {
                var list = await _teacherService.AllSubjectsTaught(teacher);
                return Ok(list);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.InnerException);
            }
        }

        //[Authorize(Roles = "Profesor")]
        [HttpPost("CreateTask")]
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
                return BadRequest("The teacher does not exist.");
            }

            var subjectExists = await _validationsManager.ValidateTeacherSubjectExistAsync(task.ProfesorId, task.MateriaId);

            if (!subjectExists)
            {
                return BadRequest("The teacher does not have this subject assigned or it does not exist.");
            }

            try
            {
                await _teacherService.CreateTask(task);
                return Ok("The task has been successfully published.");
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.InnerException);
            }
        }

        [HttpPut("UpdateSubject/{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] UpdateSubjectRequestDto updatedSubjectDto)
        {
            try
            {
                // Get the existing subject by its ID
                var existingSubject = await _teacherService.GetSubjectById<Materia>(id);
                if (existingSubject == null)
                {
                    return NotFound($"The subject with ID {id} was not found");
                }

                // Update the properties of the existing subject with the values provided in updatedSubjectDto
                existingSubject.NombreMateria = updatedSubjectDto.Titulo; // For example, if the DTO has a 'SubjectName' field

                // You can add more lines if there are more properties to update
                

                return Ok("Subject updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateSubject")]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequestDto subjectDto)
        {
            try
            {
                await _teacherService.CreateSubject(subjectDto);
                return Ok("Subject created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _teacherService.GetAllSubjects<Materia>();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetSubjectById/{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            try
            {
                var subject = await _teacherService.GetSubjectById<Materia>(id);
                if (subject == null)
                    return NotFound($"The subject with ID {id} was not found");

                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Profesor")]
        [HttpPost("CreateForum")]
        public async Task<IActionResult> CreateForum(CreateForumRequestDto forumDto)
        {
            var validation = await _validationsManager.ValidateAsync(forumDto);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }

            var teacherExists = await _validationsManager.ValidateTeacherExistAsync(forumDto.ProfesorId);

            if (!teacherExists)
            {
                return BadRequest("The teacher does not exist.");
            }

            var subjectExists = await _validationsManager.ValidateTeacherSubjectExistAsync(forumDto.ProfesorId, forumDto.MateriaId);

            if (!subjectExists)
            {
                return BadRequest("The teacher does not have this subject assigned or it does not exist.");
            }

            try
            {
                await _teacherService.CreateForum(forumDto);
                return Ok("The forum has been successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.InnerException);
            }
        }

        // GET: api/forums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Forum>>> GetAllForums()
        {
            return await _teacherService.GetAllForums();
        }

        // GET: api/forums/by-materia/{materiaId}
        [HttpGet("by-materia/{materiaId}")]
        public async Task<ActionResult<IEnumerable<Forum>>> GetForumsByMateria(int materiaId)
        {
            return await _teacherService.GetForumsByMateria(materiaId);
        }


    }

 }

