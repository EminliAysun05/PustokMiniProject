using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;

namespace Pustokk.DAL.Repositories
{
    public class ProductTagRepository : EfCoreRepository<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
