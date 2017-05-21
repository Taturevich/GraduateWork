using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Infrastructure;
using BusinessLogic.Infrastructure.DAL;

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
