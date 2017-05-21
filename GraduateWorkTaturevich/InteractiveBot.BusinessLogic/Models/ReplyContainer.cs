using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Models
{
    public class ReplyContainer
    {
        public bool IsContainsDomainData => DomainDataList.Any();

        public List<DisplayedObject> DomainDataList { get; } = new List<DisplayedObject>();
    }
}
