using System.Linq.Expressions;
using Tem.Base.Common;
using Temp.DAL.Interfaces;
using Temp.Models.Entities.Models;
using Temp.Models.Inpute;
using Temp.Models.Output;
using Temp.Services.Utilities;

namespace Temp.Services.SpeakersService
{
    public class SpeakersServices : BaseService<Speaker>, ISpeakersServices
    {
        private readonly ICacheService _cacheService;
        public SpeakersServices(IServiceProvider serviceProvider, ICacheService cacheService) : base(serviceProvider)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }
        public async Task<AppResponse> Add(SpeakerInput input)
        {
            Speaker speaker = Mapper.Map<Speaker>(input);
            await Repository.AddAsync(speaker);
            await UnitOfWork.SaveChngesAsync();

            // Caching the newly added speaker
            string cacheKey = $"Speaker_{speaker.Id}"; // Use a unique key based on your application's requirements
            await _cacheService.SetAsync(cacheKey, speaker);
            return new AppResponse();
        }

        public async Task<AppResponse> Edit(SpeakerInput input)
        {
            Speaker speaker = await Repository.GetByIdAsync(input.Id);
            if (speaker is null)
            {
                return new AppResponse("speaker not found");
            }
            Mapper.Map(input, speaker);
            await Repository.UpdateAsync(speaker);
            await UnitOfWork.SaveChngesAsync();
            await InvalidateCacheAsync(input.Id.Value);
            return new AppResponse();
        }
        public async Task<AppResponse> Delete(Guid id)
        {
            Speaker speaker = await Repository.GetByIdAsync(id);
            await Repository.RemoveAsync(speaker, false);
            await UnitOfWork.SaveChngesAsync();
            await InvalidateCacheAsync(id);
            return new AppResponse();

        }
        public async Task<AppResponse<SpekerOutput>> GetById(Guid id)
        {
            var cacheKey = $"Speaker_GetById_{id}";
            var cachedResult = await _cacheService.GetAsync<SpekerOutput>(cacheKey);
            if (cachedResult is not null)
            {
                return new AppResponse<SpekerOutput>(cachedResult);
            }
            Speaker speaker = await Repository.FindAsync(x => x.Id == id);
            SpekerOutput result = Mapper.Map<SpekerOutput>(speaker);
            await _cacheService.SetAsync(cacheKey, result);
            return new AppResponse<SpekerOutput>(result);
        }

        public async Task<AppResponse<List<Speaker>>> List()
        {
            string cacheKey = "SpeakersList";
            List<Speaker> cachedSpeakers = await _cacheService.GetAsync(cacheKey, async () =>
            {
                List<Speaker> speakersFromDataSource = await Repository.GetAllAsync();
                //List<SpekerOutput> result = Mapper.Map<List<Speaker>>(speakersFromDataSource);
                return speakersFromDataSource;
            });
            return new AppResponse<List<Speaker>>(cachedSpeakers);
        }
        public async Task<PageOutput<List<SpekerOutput>>> Search(PageInput<string> input)
        {
            string cacheKey = $"Search_{input.Data}_{input.PageSize}";
            PageOutput<List<SpekerOutput>> cachedResult = await _cacheService.GetAsync(cacheKey, async () =>
            {
                string[] includes = { };
                Expression<Func<Speaker, bool>> filter = InitFilter(input.Data);
                PagerInput paginateInput = Mapper.Map<PagerInput>(input);
                PageOutput<List<Speaker>> pageOutput = await Repository.Paginate(paginateInput, filter, includes);
                PageOutput<List<SpekerOutput>> result = Mapper.Map<PageOutput<List<SpekerOutput>>>(pageOutput);

                return result;
            });
            return cachedResult;
        }
        public async Task<AppResponse<List<SpeakerInput>>> GetList(SpeakerInput input)
        {
            string cacheKey = $"GetList_{input.Id}_{input.Name}_{input.MobileNumber}";
            List<SpeakerInput> cachedResult = await _cacheService.GetAsync(cacheKey, async () =>
            {
                Expression<Func<Speaker, bool>> filter = InitFilter(input);
                List<Speaker> speakersFromDataSource = await Repository.GetAllAsync(filter);
                List<SpeakerInput> result = Mapper.Map<List<SpeakerInput>>(speakersFromDataSource);
                return result;
            });
            return new AppResponse<List<SpeakerInput>>(cachedResult);
        }

        public Task<SpeakerInput> GetDetailsById(Guid id)
        {
            throw new NotImplementedException();
        }
        private Expression<Func<Speaker, bool>> InitFilter(string input)
        {
            Expression<Func<Speaker, bool>> filter = x => false;

            if (!input.NotIsNullOrEmpty())
                return filter = x => !x.IsDeleted;
            input = input.ContactSearchStr();
            filter = filter.OrFunc(x => x.Name.Contains(input));
            filter = filter.OrFunc(x => x.MobileNumber.Contains(input));

            return filter;
        }

        private Expression<Func<Speaker, bool>> InitFilter(SpeakerInput input)
        {
            Expression<Func<Speaker, bool>> filter = x => !x.IsDeleted;

            if (input == null)
                return filter;
            if (input.Id.HasValue)
                filter = filter.AndFunc(x => x.Id == input.Id.Value);
            if (input.Name.NotIsNullOrEmpty())
                filter = filter.AndFunc(x => x.Name.ContactSearchStr().Contains(input.Name.ContactSearchStr()));
            if (input.MobileNumber.NotIsNullOrEmpty())
                filter = filter.AndFunc(x => x.MobileNumber.ContactSearchStr().Contains(input.MobileNumber.ContactSearchStr()));
            return filter;
        }
        private async Task InvalidateCacheAsync(Guid id)
        {
            // Invalidate cache for the specified speaker
            var cacheKey = $"Speaker_GetById_{id}";
            await _cacheService.RemoveAsync(cacheKey);
            // Invalidate the general list cache
            var listCacheKey = "Speaker_List";
            await _cacheService.RemoveAsync(listCacheKey);
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
