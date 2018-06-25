using System.Linq;
using System.Data.Objects;

namespace RulesRepository.ExtensionMethods
{
    public static class Extensions
    {

        public static IQueryable<T> Include<T>(this IQueryable<T> source, string path)
        {
            var objectQuery = source as ObjectQuery<T>;

            if (objectQuery != null && !string.IsNullOrEmpty(path))
                return objectQuery.Include(path);

            return source;
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> source, string[] paths)
        {
            var objectQuery = source as ObjectQuery<T>;

            if (objectQuery != null && paths.Length > 0)
            {
                return paths.Aggregate(objectQuery, (current, path) => current.Include(path));
            }
            return source;
        }
    }
}
