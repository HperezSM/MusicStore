using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using MusicStore.Services.Interface;

namespace MusicStore.Services.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository repository;
        private readonly ILogger<SaleService> logger;
        private readonly IMapper mapper;
        private readonly IConcertRepository concertRepository;
        private readonly ICustomerRepository customerRepository;

        public SaleService(ISaleRepository repository, ILogger<SaleService> logger, IMapper mapper,
            IConcertRepository concertRepository, ICustomerRepository customerRepository)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.concertRepository = concertRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                await repository.CreateTransactionAsync();
                var entity = mapper.Map<Sale>(request);

                var customer = await customerRepository.GetByEmailAsync(email);
                if(customer is null)
                {
                    throw new InvalidOperationException($"La cuenta {email} no está registrada como cliente.");
                }

                entity.CustomerId = customer.Id;

                var concert = await concertRepository.GetAsync(request.ConcertId);
                if (concert is null)
                    throw new Exception($"El concierto con id {request.ConcertId} no existe.");

                //logica de validacion/negocio
                if (DateTime.Now > concert.DateEvent)
                    throw new InvalidOperationException($"No se puede comprar tickets para el concierto {concert.Title} porque ya es pasado.");

                if (concert.Finalized)
                    throw new InvalidOperationException($"No se puede comprar tickets para el concierto con Id {request.ConcertId} porque ya finalizó.");

                entity.Total = entity.Quantity * concert.UnitPrice;

                await repository.AddAsync(entity);
                await repository.UpdateAsync();

                response.Data = entity.Id;
                response.Success = true;

                logger.LogInformation("Se creó correctamente la venta para {email}", email);

            }
            catch (InvalidOperationException ex)
            {
                await repository.RollbackAsync();
                response.ErrorMessage = "Se esta haciendo un rollback porque ocurrió un error al guardar la venta.";
                logger.LogError(ex, "{ErrorMessage}", response.ErrorMessage);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al registrar la venta";
                logger.LogError(ex, "{ErrorMessage}", response.ErrorMessage);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<SaleResponseDto>();
            try
            {
                var sale = await repository.GetAsync(id);
                response.Data = mapper.Map<SaleResponseDto>(sale);
                response.Success = response.Data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la venta.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(SaleByDateSearchDto search, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();
            try
            {
                var dateStart = Convert.ToDateTime(search.DateStart);
                var dateEnd = Convert.ToDateTime(search.DateEnd);

                var data = await repository.GetAsync(
                    predicate: s => s.SaleDate >= dateStart && s.SaleDate <= dateEnd,
                    orderBy: x => x.OperationNumber,
                    pagination
                    );

                response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al filtrar las ventas por fecha.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(string email, string title, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Customer.Email == email && s.Concert.Title.Contains(title ?? string.Empty),
                    orderBy: x => x.SaleDate,
                    pagination
                    );

                response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al filtrar las ventas del usuario por título del evento.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<SaleReportResponseDto>>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd)
        {
            var response = new BaseResponseGeneric<ICollection<SaleReportResponseDto>>();
            try
            {
                // Codigo
                var list = await repository.GetSaleReportAsync(dateStart, dateEnd);
                response.Data = mapper.Map<ICollection<SaleReportResponseDto>>(list);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener los datos del reporte";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
