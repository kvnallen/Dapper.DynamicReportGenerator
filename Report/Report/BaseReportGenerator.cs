using System.Collections.Generic;
using System.Linq;

namespace DapperDynamic.Report
{
    internal static class BaseReportGenerator
    {
        public static ICollection<string> GetKeys(this IDictionary<string, object> firstRow)
        {
            return firstRow.Keys;
        }

        public static string Join(this IEnumerable<string> list, string splitter)
        {
            return string.Join(splitter, list);
        }
    }
}