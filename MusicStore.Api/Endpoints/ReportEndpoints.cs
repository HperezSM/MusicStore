using Microsoft.AspNetCore.Authorization;
using MusicStore.Services.Interface;

namespace MusicStore.Api.Endpoints
{
    public static class ReportEndpoints
    {
        public static void MapReports(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/Reports")
                .WithDescription("Reportes de Music Store")
                .WithTags("Reports");

            group.MapGet("/", [Authorize(Roles = Entities.Constants.RoleAdmin)] async (ISaleService service, string dateStart, string dateEnd) =>
            {
                var response = await service.GetSaleReportAsync(DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });
        }
    }
}
