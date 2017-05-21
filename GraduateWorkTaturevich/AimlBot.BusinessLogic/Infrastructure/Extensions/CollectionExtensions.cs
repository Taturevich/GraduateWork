using System.Collections.Generic;
using System.Data;
using FastMember;

namespace BusinessLogic.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static DataTable ToDataTable<T>(this List<T> collection)
        {
            var table = new DataTable();
            using (var reader = ObjectReader.Create(collection))
            {
                table.Load(reader);
            }

            return table;
        }
    }
}
