using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services.Interface
{
    public interface IConcertService
    {
        Task<BaseResponseGeneric<ICollection<ConcertResponseDto>>> GetAsync(string? title, PaginationDto pagination);
        Task<BaseResponseGeneric<ConcertResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<int>> AddAsync(ConcertRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> FinalizeAsync(int id);

    }
}
