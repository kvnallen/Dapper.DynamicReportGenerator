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

                var header = GetHeader(firstRow, splitter);

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
            var builder = new StringBuilder();
            var rowsWithValues = dapperRows.Select(x => x.Values);

            foreach (var row in rowsWithValues)
            {
                foreach (var rowValue in row)
                    builder.Append(rowValue + splitter);

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        private static string GetHeader(IDictionary<string, object> firstRow, string splitter)
        {
            return firstRow.Keys.Aggregate((agg, next) => agg + splitter + next);
        }
    }
}
