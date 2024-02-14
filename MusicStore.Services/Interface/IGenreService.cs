using MusicStore.Dto;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;

namespace MusicStore.Services.Interface
{
    public interface IGenreService
    {
        Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);

    }
}
