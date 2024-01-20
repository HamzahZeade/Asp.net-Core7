using Temp.Models.Entities.Models;
using Temp.Models.Inpute;
using Temp.Models.Output;

namespace Temp.Services.SpeakersService
{
    public interface ISpeakersServices
    {

        Task<AppResponse> Add(SpeakerInput input);
        Task<AppResponse> Edit(SpeakerInput input);
        Task<AppResponse> Delete(Guid id);
        Task<SpeakerInput> GetDetailsById(Guid id);
        Task<AppResponse<SpekerOutput>> GetById(Guid id);
        Task<AppResponse<List<SpeakerInput>>> GetList(SpeakerInput input);
        Task<AppResponse<List<Speaker>>> List();
        Task<PageOutput<List<SpekerOutput>>> Search(PageInput<string> input);
    }
}
