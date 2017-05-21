using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimlBotUI.Infrastructure
{
    public interface IChild : ICloneable
    {
        int Id { get; set; }

        string Description { get; set; }
    }
}
