using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Interfaces
{
    public interface ITeacherService
    {
        Task CreateSubject(CreateSubjectRequestDto subjectDto);
        Task<T> GetSubjectById<T>(int id);
        Task<List<T>> GetAllSubjects<T>();
        Task TeachMatterSubject(TeachMatterRequestDto teachMatter);

        Task<List<object>> AllSubjectsTaught(AllSubjectsTaughtRequestDto teacher);

        Task CreateTask(TaskPublishRequestDto newTask);
        Task UpdateSubject(int id, UpdateSubjectRequestDto updatedSubject);


    }
}
