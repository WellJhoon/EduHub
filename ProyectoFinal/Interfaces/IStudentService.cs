using ProyectoFinal.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ProyectoFinal.DTOs.StudentDTO;

namespace ProyectoFinal.Interfaces
{
    public interface IStudentService
    {
        Task EnrollCareerStudent(EnrollCareerStudentRequestDto student);

        Task EnrollSubjectStudent(EnrollSubjectStudentRequestDto student);

        Task<List<object>> SubjectsEnrolledByStudent(AllSubjectsStudentRequestDto student);

        Task<List<object>> GetAllAssignmentsForStudent(AllSubjectsStudentRequestDto student);

        Task<bool> SubmitAssignment(AssignmentDto assignmentDto);

        Task<List<object>> GetPendingAssignmentsForStudent(AllSubjectsStudentRequestDto student);

    }
}
