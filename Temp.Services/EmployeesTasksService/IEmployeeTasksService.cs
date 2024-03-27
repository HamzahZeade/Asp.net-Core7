using Temp.Models.Dtos;
using Temp.Models.Output;

namespace Temp.Services.EmployeesTasksService
{
    public interface IEmployeeTasksService
    {
        Task<AppResponse<List<TaskDto>>> List();
        Task<AppResponse> Add(TaskInput input);
    }
}
