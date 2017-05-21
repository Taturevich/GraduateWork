using System;
using System.Collections.Generic;

namespace BusinessLogic.BotConfiguration.QueryDomainBuilder
{
    public class EntityMatcher
    {
        public EntityMatcher(Type entityType)
        {
            GetEntityType = entityType;
        }

        public string TableName { get; set; }

        public Type GetEntityType { get; }

        public HashSet<string> MatchedTags { get; } = new HashSet<string>();

        public HashSet<string> MatchedBulkTags { get; } = new HashSet<string>();
    }
}
