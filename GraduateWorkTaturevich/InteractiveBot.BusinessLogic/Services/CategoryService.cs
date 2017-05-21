using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Infrastructure;
using BusinessLogic.Infrastructure.DAL;

namespace BusinessLogic.Services
{
    public interface ICategoryService : IEntityServiceBase<Category>
    {
    }
    internal class CategoryService : EntityServiceBase<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository)
            : base(repository)
        {
        }
    }
}
