using FluentValidation;
using ProyectoFinal.Validations;
using static ProyectoFinal.DTOs.UsuarioDTO;
using static ProyectoFinal.DTOs.StudentDTO;
using static ProyectoFinal.Validations.StudentValidation;
using static ProyectoFinal.DTOs.TeacherDTO;
using static ProyectoFinal.Validations.TeacherValidation;


namespace ProyectoFinal.Configuration
{
    public static class ValidatorsDependencies
    {
        public static void ValidatorsInjections(this IServiceCollection services)
        {
            //User
            services.AddScoped<IValidator<RegisterUserRequestDto>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserRequestDto>, LoginUserValidator>();
            services.AddScoped<IValidator<UpdateUserRequestDto>, UpdateUserValidator>();
            services.AddScoped<IValidator<DeleteUserRequestDto>, DeleteUserValidator>();
            services.AddScoped<IValidator<AssignPermissionsUserRequestDto>, AssignUserValidator>();
            services.AddScoped<IValidator<EnrollCareerStudentRequestDto>, EnrollCareerStudentValidator>();
            services.AddScoped<IValidator<EnrollSubjectStudentRequestDto>, EnrollSubjectStudentValidator>();
            services.AddScoped<IValidator<TeachMatterRequestDto>, TeachMatterValidator>();
            services.AddScoped<IValidator<AllSubjectsTaughtRequestDto>, AllSubjectsTaughtValidator>();
            services.AddScoped<IValidator<AllSubjectsStudentRequestDto>, AllSubjectsStudentValidator>();
            services.AddScoped<IValidator<TaskPublishRequestDto>, TaskPublishValidator>();






        }
    }
}
