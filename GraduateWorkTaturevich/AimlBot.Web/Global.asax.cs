using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AimlBotWeb.EventLogs;
using AimlBotWeb.Features.Categories;
using AimlBotWeb.Features.Products;
using AimlBotWeb.Messages;
using AutoMapper;
using BusinessLogic.Entities.FactoryDomain;
using BusinessLogic.Entities.Infrastructure;

namespace AimlBotWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Message, MessageModel>();
                cfg.CreateMap<MessageModel, Message>();
                cfg.CreateMap<EventLog, EventLogModel>();
                cfg.CreateMap<EventLogModel, EventLog>();
                cfg.CreateMap<Product, ProductModel>();
                cfg.CreateMap<ProductModel, Product>();
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<CategoryModel, Category>();
            });
        }
    }
}
