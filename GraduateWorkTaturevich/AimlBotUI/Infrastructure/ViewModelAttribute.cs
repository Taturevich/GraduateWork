using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimlBotUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewModelAttribute : Attribute
    {
        public string Qualifier { get; }

        public ViewModelAttribute()
        {
        }

        public ViewModelAttribute(string qualifier)
        {
            Qualifier = qualifier;
        }
    }
}
