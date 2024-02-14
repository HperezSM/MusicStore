using MusicStore.Dto.Request;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using System.Linq.Expressions;

namespace MusicStore.Repositories
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollbackAsync();
        Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate, Expression<Func<Sale, TKey>> orderBy, PaginationDto pagination);
        Task<ICollection<ReportInfo>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd);
    }
}
