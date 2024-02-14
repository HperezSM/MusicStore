using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Entities;
using MusicStore.Entities.Info;
using MusicStore.Persistence;
using MusicStore.Repositories.Utils;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace MusicStore.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        private readonly IHttpContextAccessor contextAccessor;

        public SaleRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor) : base(context)
        {
            this.contextAccessor = contextAccessor;
        }

        public async Task CreateTransactionAsync()
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        public override async Task<int> AddAsync(Sale entity)
        {
            entity.SaleDate = DateTime.Now;
            var nextNumber = await context.Set<Sale>().CountAsync() + 1;
            entity.OperationNumber = $"{nextNumber:000000}";

            await context.AddAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<Sale?> GetAsync(int id)
        {
            return await context.Set<Sale>()
                .Include(x => x.Customer)
                .Include(x => x.Concert)
                .ThenInclude(x => x.Genre)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task RollbackAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public async Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate, Expression<Func<Sale, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Sale>()
                .Include(x => x.Customer)
                .Include(x => x.Concert)
                .ThenInclude(x => x.Genre)
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await contextAccessor.HttpContext.InsertPaginationHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();
            return response;
        }
        public async Task<ICollection<ReportInfo>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd)
        {
            //Raw query
            var query = context.Set<ReportInfo>().FromSqlRaw(
                @"select c.Title [ConcertName], sum(s.Total) [Total] from Musicales.Sale s
                inner join Musicales.Concert c on c.Id = s.ConcertId
                where s.SaleDate >= {0} and s.SaleDate <= {1}
                group by c.Title",
                dateStart, dateEnd);
            return await query.ToListAsync();
        }
    }
}
