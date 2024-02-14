using MusicStore.Dto.Request;
using MusicStore.Services.Interface;

namespace MusicStore.Api.Endpoints
{
    public static class HomeEndpoints
    {
        public static void MapHomeEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Home", async (IConcertService concertService, IGenreService genreService) =>
            {
                var concerts = await concertService.GetAsync("", new PaginationDto
                {
                    Page = 1,
                    RecordsPerPage = 10
                });

                var genres = await genreService.GetAsync();

                return Results.Ok(new
                {
                    Concerts = concerts.Data,
                    Genres = genres.Data,
                    Success = true
                });
            }).WithDescription("Permite mostrar los endpoints de la pagina principal");
        }
    }
}
