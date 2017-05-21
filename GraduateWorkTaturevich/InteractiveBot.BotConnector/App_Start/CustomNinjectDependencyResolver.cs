using System.Web.Http.Dependencies;
using Ninject;

namespace TestBotConnection
{
    public class CustomNinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        readonly IKernel _kernel;

        public CustomNinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this._kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}