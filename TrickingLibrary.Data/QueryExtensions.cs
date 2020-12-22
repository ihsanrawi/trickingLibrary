using System.Linq;
using TrickingLibrary.Models.Abstractions;

namespace TrickingLibrary.Data
{
    public static class QueryExtensions
    {
        public static int LatestVersion<TSource>(this IQueryable<TSource> source, int offset)
            where TSource : VersionedModel => source.Max(x => x.Version) + offset;
    }
}