using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.BotConfiguration.QueryDomainBuilder
{
    public class EntityMatcherProvider
    {
        public EntityMatcherProvider(IEnumerable<EntityMatcher> entityMatchers)
        {
            EntityMatchers.AddRange(entityMatchers);
        }

        public List<EntityMatcher> EntityMatchers { get; } = new List<EntityMatcher>();

        public EntityMatcher TryGetEntityTypeByTag(string text)
        {
            var foundMatcher = EntityMatchers
                .FirstOrDefault(x => x.MatchedTags.Contains(text)
                    || x.MatchedBulkTags.Contains(text));
            return foundMatcher;
        }
    }
}
