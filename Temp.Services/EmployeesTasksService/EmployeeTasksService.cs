using Tem.Base.Common;
using Temp.DAL.Interfaces;
using Temp.Models.Dtos;
using Temp.Models.Entities.Models;
using Temp.Models.Output;

namespace Temp.Services.EmployeesTasksService
{


    public class EmployeeTasksService : BaseService<MyTask>, IEmployeeTasksService
    {
        private readonly ICacheService _cacheService;
        public EmployeeTasksService(IServiceProvider serviceProvider, ICacheService cacheService) : base(serviceProvider)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        //public async Task<AppResponse<List<MyTask>>> List()
        //{
        //    List<MyTask> MyTaskDataSource = await Repository.GetAllAsync();
        //    return new AppResponse<List<MyTask>>(MyTaskDataSource);
        //}
        public async Task<AppResponse> Add(TaskInput input)
        {
            MyTask task = Mapper.Map<MyTask>(input);
            await Repository.AddAsync(task);
            await UnitOfWork.SaveChngesAsync();

            // Caching the newly added speaker
            string cacheKey = $"MyTask_{task.Id}"; // Use a unique key based on your application's requirements
            await _cacheService.SetAsync(cacheKey, task);
            return new AppResponse();
        }
        public async Task<AppResponse<List<TaskDto>>> List()
        {

            List<MyTask> tasks = await Repository.GetAllAsync();
            List<TaskDto> taskDtos = tasks.ConvertAll(ConvertToTaskDto);
            return new AppResponse<List<TaskDto>>(taskDtos);


        }

        private TaskDto ConvertToTaskDto(MyTask myTask)
        {
            return new TaskDto
            {
                id = myTask.Id,
                text = myTask.Text,
                company = myTask.Company,
                priority = myTask.Priority.ToString(),
                startDate = myTask.StartDate,
                dueDate = myTask.DueDate,
                owner = myTask.Owner,
                status = myTask.Status.ToString(),
                parentId = myTask?.ParentId,
                progress = myTask?.Progress,


                // Add mapping for other properties as needed
            };
        }




        //public async Task<AppResponse<List<SpeakerInput>>> GetList(SpeakerInput input)
        //{
        //    Expression<Func<Organization, bool>> filter = InitFilter(input);
        //    List<Organization> org = await Repository.GetAllAsync(filter);
        //    List<OrganizationDto> result = Mapper.Map<List<OrganizationDto>>(org);
        //    return new AppResponse<List<OrganizationDto>>(result);
        //}

    }
}
