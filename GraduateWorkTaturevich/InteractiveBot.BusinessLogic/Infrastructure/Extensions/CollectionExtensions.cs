using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public static object ToTypedList(this IEnumerable<object> value, Type type)
        {
            var convertedValue = value.Select(item => Convert.ChangeType(item, type)).Where(x => true).ToList();
            Console.WriteLine(convertedValue);
            return convertedValue;
        }
    }
}
