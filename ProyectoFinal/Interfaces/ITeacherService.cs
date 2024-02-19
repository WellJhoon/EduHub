using ProyectoFinal.Models;
using static ProyectoFinal.DTOs.TeacherDTO;

namespace ProyectoFinal.Interfaces
{
    public interface ITeacherService
    {
        Task TeachMatterSubject(TeachMatterRequestDto teachMatter);

        Task<List<object>> AllSubjectsTaught(AllSubjectsTaughtRequestDto teacher);

        Task CreateTask(TaskPublishRequestDto newTask);
    }
}
