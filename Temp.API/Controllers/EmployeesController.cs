using Microsoft.AspNetCore.Mvc;
using Temp.Models.Dtos;
using Temp.Models.Output;
using Temp.Services.EmployeesTasksService;

namespace Temp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class EmployeesController : BaseController<IEmployeeTasksService>
    {
        public EmployeesController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        [HttpGet, Route("AllTasks")]
        [ProducesResponseType(typeof(AppResponse<List<TaskDto>>), 200)]
        public async Task<IActionResult> List()
        {
            AppResponse<List<TaskDto>> appResponse = await CurrentService.List();
            return Ok(appResponse);
        }
        [HttpPost, Route("SaveTask")]
        [ProducesResponseType(typeof(AppResponse), 200)]
        public async Task<IActionResult> Add([FromBody] TaskInput input)
        {
            AppResponse appResponse = await CurrentService.Add(input);
            return Ok(appResponse);
        }
        //[HttpPost ,Route("SaveTask")]
        //public async Task<IActionResult> Post([FromBody] Task task)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Tasks.Add(task);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtRoute("GetTask", new { id = task.Id }, task);
        //}

    }
}
