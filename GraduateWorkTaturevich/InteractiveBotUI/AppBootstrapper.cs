using AimlBotUI.Infrastructure;
using AimlBotUI.Shared;
using AimlBotUI.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using AimlBotUI.Views.Users;
using BusinessLogic.Infrastructure;
using Castle.Core.Internal;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception;

namespace AimlBotUI
{
    /// <summary>
    /// Dependency injection startup class
    /// </summary>
    public class AppBootstrapper : BootstrapperBase
    {
        ///private IUnityContainer container;
        private IKernel _kernel;
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            MessageBinder.SpecialValues.Add("$originalsourcecontext", context =>
            {
                var args = context.EventArgs as RoutedEventArgs;

                var fe = args?.OriginalSource as FrameworkElement;

                return fe?.DataContext;
            });

            ConfigureKeyBinding();
            _kernel =
                new StandardKernel(
                    new NinjectSettings
                    {
                        LoadExtensions = false,
                        InjectAttribute = typeof(BusinessLogic.Infrastructure.InjectAttribute)
                    },
                    new FuncModule(), new DynamicProxyModule());
            Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.IsDefined(typeof(ViewModelAttribute))
                                && typeof(IViewModel).IsAssignableFrom(x)
                                && x.IsClass
                                && !x.IsAbstract)
                    .ForEach(x =>
                    {
                        if (x != null)
                        {
                            _kernel.Bind<IViewModel>().To(x);
                        }
                    });
            _kernel.Bind<IWindowManager>().To<WindowManager>();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>();
            _kernel.Bind<IUsersViewModelFactory>().ToFactory();
            _kernel.Load(new BusinessLogicModule());
        }

        private static void ConfigureKeyBinding()
        {
            var trigger = Parser.CreateTrigger;

            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    ElementConvention defaults = ConventionManager.GetElementConvention(target.GetType());
                    return defaults.CreateTrigger();
                }

                string triggerDetail = triggerText.Replace("[", string.Empty).Replace("]", string.Empty);

                string[] splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (splits[0] == "Key")
                {
                    var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                    return new KeyTrigger { Key = key };
                }

                return trigger(target, triggerText);
            };
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {

            DisplayRootViewFor<BotChatViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return _kernel.Get(service, key);
            }

            return _kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }
    }
}
