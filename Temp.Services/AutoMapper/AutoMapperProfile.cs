using AutoMapper;
using Temp.Models.Entities.Models;
using Temp.Models.Inpute;

namespace Temp.Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SpeakerInput, Speaker>(); // Map properties with the same name
        }
    }
}
