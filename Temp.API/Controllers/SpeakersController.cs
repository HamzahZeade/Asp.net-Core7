using Microsoft.AspNetCore.Mvc;
using Temp.Models.Entities.Models;
using Temp.Models.Inpute;
using Temp.Models.Output;
using Temp.Services.SpeakersService;

namespace Temp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SpeakersController : BaseController<ISpeakersServices>
    {
        public SpeakersController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpPost, Route("add")]
        [ProducesResponseType(typeof(AppResponse), 200)]
        public async Task<IActionResult> Add([FromBody] SpeakerInput input)
        {
            AppResponse appResponse = await CurrentService.Add(input);
            return Ok(appResponse);
        }

        [HttpPost, Route("edit")]
        [ProducesResponseType(typeof(AppResponse), 200)]
        public async Task<IActionResult> Edit([FromBody] SpeakerInput input)
        {
            AppResponse appResponse = await CurrentService.Edit(input);
            return Ok(appResponse);
        }


        [HttpPost, Route("delete/{id}")]
        [ProducesResponseType(typeof(AppResponse), 200)]
        public async Task<IActionResult> Delete(Guid id)
        {
            AppResponse appResponse = await CurrentService.Delete(id);
            return Ok(appResponse);
        }

        [HttpGet, Route("getById/{id}")]
        [ProducesResponseType(typeof(AppResponse<SpekerOutput>), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            AppResponse<SpekerOutput> appResponse = await CurrentService.GetById(id);
            return Ok(appResponse);
        }


        [HttpPost, Route("getList")]
        [ProducesResponseType(typeof(AppResponse<List<Speaker>>), 200)]
        public async Task<IActionResult> List()
        {
            AppResponse<List<Speaker>> appResponse = await CurrentService.List();
            return Ok(appResponse);
        }

        [HttpPost, Route("Search")]
        [ProducesResponseType(typeof(PageOutput<List<SpekerOutput>>), 200)]
        public async Task<IActionResult> GetPagedList([FromBody] PageInput<string> input)
        {
            PageOutput<List<SpekerOutput>> appResponse = await CurrentService.Search(input);
            return Ok(appResponse);
        }


    }
}
