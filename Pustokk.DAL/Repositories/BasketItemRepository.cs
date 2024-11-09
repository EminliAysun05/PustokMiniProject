using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.DAL.Repositories.Contracts;

namespace Pustokk.DAL.Repositories;

public class BasketItemRepository : EfCoreRepository<BasketItem>, IBasketItemRepository
{
    public BasketItemRepository(AppDbContext context) : base(context)
    {
    }
}
