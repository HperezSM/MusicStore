using AutoMapper;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Info;

namespace MusicStore.Services.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertInfo, ConcertResponseDto>();//origin -> destination
            CreateMap<Concert, ConcertResponseDto>()
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.DateEvent.ToShortDateString()))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.DateEvent.ToShortTimeString()))
                .ForMember(d => d.Status, o => o.MapFrom(x => x.Status ? "Activo" : "Inactivo"));

            CreateMap<ConcertRequestDto, Concert>()
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => Convert.ToDateTime($"{x.DateEvent} {x.TimeEvent}")))
                .ForMember(d => d.ImageUrl, options => options.Ignore());
        }
    }
}
