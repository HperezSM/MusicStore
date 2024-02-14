using AutoMapper;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using System.Globalization;

namespace MusicStore.Services.Profiles
{
    public class SaleProfile : Profile
    {
        private static readonly CultureInfo culture = new("es-PE");
        public SaleProfile()
        {
            CreateMap<SaleRequestDto, Sale>()
                .ForMember(d => d.Quantity, o => o.MapFrom(x => x.TicketsQuantity));

            CreateMap<ReportInfo, SaleReportResponseDto>();

            CreateMap<Sale, SaleResponseDto>()
                .ForMember(d => d.SaleId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("D", culture)))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("T", culture)))
                .ForMember(d => d.Genre, o => o.MapFrom(x => x.Concert.Genre.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.Concert.ImageUrl))
                .ForMember(d => d.Title, o => o.MapFrom(x => x.Concert.Title))
                .ForMember(d => d.FullName, o => o.MapFrom(x => x.Customer.FullName))
                .ForMember(d => d.SaleDate, o => o.MapFrom(x => x.SaleDate.ToString("dd/MM/yyyy HH:mm", culture)));
        }
    }
}
