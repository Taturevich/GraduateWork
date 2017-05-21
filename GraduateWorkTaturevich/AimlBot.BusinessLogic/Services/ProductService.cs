using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Infrastructure;

namespace BusinessLogic.Services
{
    public interface IProductService : IEntityServiceBase<Product>
    {
    }
    internal class ProductService : EntityServiceBase<Product>, IProductService
    {
        public ProductService(IRepository<Product> repository)
            : base(repository)
        {
        }
    }
}
