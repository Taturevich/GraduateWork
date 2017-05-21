using System.Collections;
using System.Linq;
using System.Reflection;
using AimlBotUI.Shared;

namespace AimlBotUI.Infrastructure
{
    public abstract class ChildViewModelBase : ScreenBase, IViewModel, IChild, IMutateMode
    {
        public object Clone()
        {
            return CloneInternal();
        }

        protected abstract object CloneInternal();

        protected void CopyProperties(object source, object dest, params string[] exceptions)
        {
            if (source == null || dest == null)
            {
                return;
            }

            var sourceType = source.GetType();
            var destType = dest.GetType();
            var properties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => exceptions.All(p => x.Name != p));
            foreach (var sourceProp in properties)
            {
                var destProp = destType.GetProperty(sourceProp.Name);

                if (destProp == null || !destProp.CanWrite || IsCollection(destProp))
                {
                    continue;
                }

                destProp.SetValue(dest, sourceProp.GetValue(source), null);
            }
        }

        private static bool IsCollection(PropertyInfo objType)
        {
            return objType.PropertyType.GetInterfaces().Any(x => x == typeof(IEnumerable)) && (objType.PropertyType != typeof(string));
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsNewInstance => Id == 0;
    }
}
