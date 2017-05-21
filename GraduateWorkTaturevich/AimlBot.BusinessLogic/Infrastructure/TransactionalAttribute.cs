using System;
using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace BusinessLogic.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class TransactionalAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<ITransactionalInterceptor>();
        }
    }

    public interface ITransactionalInterceptor : IInterceptor
    {
    }

    internal class TransactionalInterceptor : ITransactionalInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            using (var context = new BlDbContext("Server=tcp:taturevichserver.database.windows.net,1433;" +
                                                 "Initial Catalog=NewDb;Persist Security Info=False;" +
                                                 "User ID=ivan_taturevich;Password=Aa83386491994;" +
                                                 "MultipleActiveResultSets=False;Encrypt=True;" +
                                                 "TrustServerCertificate=False;Connection Timeout=30;"))
            {
                invocation.Proceed();
                context.SaveChanges();
            }
        }
    }
}
