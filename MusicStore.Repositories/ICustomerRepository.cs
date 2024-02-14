using MusicStore.Entities;

namespace MusicStore.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
    }
}
