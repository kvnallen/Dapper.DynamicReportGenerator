using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperDynamic.Report
{
    public static class CsvGenerator
    {
        /// <summary>
        /// Return the report in byteArray (CSV Format)
        /// </summary>
        /// <param name="data">Result of Dapper Query</param>
        /// <param name="splitter">Splitter, defaut is ";"</param>
        /// <returns></returns>
        public static byte[] GetByteArrayReport(IEnumerable<dynamic> data, string splitter = ";")
        {
            return Encoding.Default.GetBytes(GetStringReport(data, splitter));
        }

        /// <summary>
        /// Return the report in string (CSV Format)
        /// </summary>
        /// <param name="data">Result of Dapper Query</param>
        /// <param name="splitter">Splitter, defaut is ";"</param>
        /// <returns></returns>
        public static string GetStringReport(IEnumerable<dynamic> data, string splitter = ";")
        {
            try
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var builder = new StringBuilder();

                var dapperRows = data.Cast<IDictionary<string, object>>().ToList();
                var firstRow = dapperRows.First();

                var header = firstRow.GetKeys()
                                      .Aggregate((agg, next) => agg + splitter + next);

                builder.Append(header + Environment.NewLine);
                builder.Append(GetValues(dapperRows, splitter));

                return builder.ToString();
            }
            catch (InvalidOperationException e)
            {
                return string.Empty;
            }
        }

        private static string GetValues(IEnumerable<IDictionary<string, object>> dapperRows, string splitter)
        {
            var rowValues = dapperRows.Select(x => x.Values);
            var values = rowValues
                .Select(dictionary => GetValueLine(dictionary, splitter))
                .ToArray();

            return string.Join(Environment.NewLine, values);
        }

        private static string GetValueLine(ICollection<object> dictionary, string splitter) => $"{string.Join(splitter, dictionary)}{splitter}";
    }
}
