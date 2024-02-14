using MusicStore.Dto.Request;
using MusicStore.Entities;
using MusicStore.Entities.Info;

namespace MusicStore.Repositories
{
    public interface IConcertRepository : IRepositoryBase<Concert>
    {
        Task<ICollection<ConcertInfo>> GetAsync(string? title, PaginationDto pagination);
        Task FinalizeAsync(int id);
    }
}
