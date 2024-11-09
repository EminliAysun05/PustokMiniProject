using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;

namespace Pustokk.DAL.Repositories
{
    public class ServiceRepository : EfCoreRepository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
