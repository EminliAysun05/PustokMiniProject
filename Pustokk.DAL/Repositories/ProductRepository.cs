using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;

namespace Pustokk.DAL.Repositories;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
