using FluentValidation;
using static ProyectoFinal.DTOs.StudentDTO;

namespace ProyectoFinal.Validations
{
    public class StudentValidation
    {
        public class EnrollCareerStudentValidator : AbstractValidator<EnrollCareerStudentRequestDto>
        {
            public EnrollCareerStudentValidator()
            {
                RuleFor(x => x.EstudianteId).GreaterThan(0);
                RuleFor(x => x.CarreraId).InclusiveBetween(1, 6);

            }
        }

        public class EnrollSubjectStudentValidator : AbstractValidator<EnrollSubjectStudentRequestDto>
        {
            public EnrollSubjectStudentValidator()
            {
                RuleFor(x => x.EstudianteId).GreaterThan(0);
                RuleFor(x => x.MateriaId).InclusiveBetween(1, 28);

            }
        }

        public class AllSubjectsStudentValidator : AbstractValidator<AllSubjectsStudentRequestDto>
        {
            public AllSubjectsStudentValidator()
            {
                RuleFor(x => x.EstudianteId).GreaterThan(0);
            }
        }
    }
}
