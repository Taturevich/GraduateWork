using System;

namespace BusinessLogic.Infrastructure.Injection
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
    }
}
