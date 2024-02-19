using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using static ProyectoFinal.DTOs.UsuarioDTO;
using static ProyectoFinal.DTOs.StudentDTO;
using ProyectoFinal.Context;
using ProyectoFinal.Interfaces;
using static ProyectoFinal.DTOs.TeacherDTO;


namespace ProyectoFinal.Validations
{

    public class ValidationsManager : IValidationsManager
    {

            private readonly EduAsyncHubContext _context;
            private readonly Dictionary<Type, IValidator> _dictionary;

            public ValidationsManager(
                EduAsyncHubContext context,
                IValidator<RegisterUserRequestDto> validatorRegisterUser,
                IValidator<LoginUserRequestDto> validatorLoginUser,
                IValidator<UpdateUserRequestDto> validatorUpdateUser,
                IValidator<DeleteUserRequestDto> validatorDeleteUser,
                IValidator<AssignPermissionsUserRequestDto> validateAssignPermissions,
                IValidator<EnrollCareerStudentRequestDto> validateEnrollCareerStudent,
                IValidator<EnrollSubjectStudentRequestDto> validateEnrollSubjectStudent,
                IValidator<TeachMatterRequestDto> validateTeachMatter,
                IValidator<AllSubjectsTaughtRequestDto> validateAllSubjectsTaught,
                IValidator<AllSubjectsStudentRequestDto> validatellSubjectsStudent,
                IValidator<TaskPublishRequestDto> validateTaskPublish










                )
        {
                _context = context;
                _dictionary = new()
            {
                { typeof(RegisterUserRequestDto), validatorRegisterUser },
                { typeof(LoginUserRequestDto), validatorLoginUser },
                { typeof(UpdateUserRequestDto), validatorUpdateUser },
                { typeof(DeleteUserRequestDto), validatorDeleteUser },
                { typeof(AssignPermissionsUserRequestDto), validateAssignPermissions },
                { typeof(EnrollCareerStudentRequestDto), validateEnrollCareerStudent },
                { typeof(EnrollSubjectStudentRequestDto), validateEnrollSubjectStudent },
                { typeof(TeachMatterRequestDto), validateTeachMatter },
                { typeof(AllSubjectsTaughtRequestDto), validateAllSubjectsTaught },
                { typeof(AllSubjectsStudentRequestDto), validatellSubjectsStudent },
                { typeof(TaskPublishRequestDto), validateTaskPublish }

            };
            }

            public async Task<ValidationResult> ValidateAsync<T>(T entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                if (_dictionary.TryGetValue(typeof(T), out var value) && value is IValidator<T> validator)
                {
                    var result = await validator.ValidateAsync(entity);
                    return result;
                }

                throw new InvalidOperationException($"Validator not registered for type {typeof(T)}. Please register a validator for this type.");
            }

        public async Task<bool> ValidateUserExistAsync(int userId)
        {
            var accountExists = await _context.Usuarios.AnyAsync(account => account.UsuarioId == userId);

            return accountExists;
        }

        public async Task<bool> ValidateStudentExistAsync(int studentId)
        {
            var studentExist = await _context.Estudiantes.AnyAsync(u => u.EstudianteId == studentId);

            return studentExist;
        }

        public async Task<bool> ValidateTeacherExistAsync(int teacherId)
        {
            var teacherExist = await _context.Profesores.AnyAsync(u => u.ProfesorId == teacherId);

            return teacherExist;
        }
        public async Task<bool> ValidateTeacherSubjectExistAsync(int professorId, int subjectId)
        {
            var subjectTeacherExist = await _context.ProfesorMateria
                .AnyAsync(pm => pm.ProfesorId == professorId && pm.MateriaId == subjectId);

            return subjectTeacherExist;
        }

        public async Task<bool> ValidateStudentSubjectExistAsync(int studentId, int subjectId)
        {
            var subjectStudentExist = await _context.EstudianteMateria
                .AnyAsync(pm => pm.EstudianteId == studentId && pm.MateriaId == subjectId);

            return subjectStudentExist;
        }


        public async Task<bool> ValidateEmailExistAsync(string email)
            {
                var emailExists = await _context.Usuarios.AnyAsync(account => account.CorreoElectronico == email);

                return emailExists;
            }



    }
}

