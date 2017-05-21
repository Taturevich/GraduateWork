using System.Collections.Generic;
using BusinessLogic.BotConfiguration.QueryDomainBuilder;
using BusinessLogic.Entities.FactoryDomain;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace BusinessLogic.Infrastructure
{
    public class BusinessLogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind(typeof(IEntityServiceBase<>)).To(typeof(EntityServiceBase<>));
            Bind<BlDbContext>().ToSelf();
            Bind<EventLogInterceptor>().ToSelf();
            Bind<ITransactionalInterceptor>().To<TransactionalInterceptor>();
            Bind<EntityMatcherProvider>()
                .ToConstant(new EntityMatcherProvider(InitializeEntitymatchers()))
                .InSingletonScope();
            Kernel.Bind(x => x.FromThisAssembly()
                .IncludingNonePublicTypes()
                .SelectAllClasses()
                .InheritedFrom(typeof(EntityServiceBase<>))
                .BindDefaultInterfaces());
        }

        private IEnumerable<EntityMatcher> InitializeEntitymatchers()
        {
            var productMatcher = new EntityMatcher(typeof(Product))
            {
                TableName = "Products"
            };
            productMatcher.MatchedTags.UnionWith(new[]
            {
                "товар",
                "продукт",
                "покупка"
            });
            productMatcher.MatchedBulkTags.UnionWith(new[]
            {
                "товары",
                "продукты",
                "покупки"
            });


            var categoryMatcher = new EntityMatcher(typeof(Category))
            {
                TableName = "Categories"
            };
            categoryMatcher.MatchedTags.UnionWith(new[]
            {
                "категория",
                "вид",
                "ассортимент",
                "разновидность"
            });
            categoryMatcher.MatchedBulkTags.UnionWith(new[]
            {
                "категории",
                "виды",
                "разновидности"
            });

            return new[] { productMatcher, categoryMatcher };
        }
    }
}
