using System.Linq;

namespace SportsStore.Models
{
    interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}
