using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;

namespace Pustokk.DAL.Repositories
{
    public class SubscribeRepository : EfCoreRepository<Subscribe>, ISubscribeRepository
    {
        public SubscribeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
