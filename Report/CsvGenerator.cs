using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Report
{
    public class CsvGenerator
    {
        private readonly IEnumerable<dynamic> _data;

        public CsvGenerator(IEnumerable<dynamic> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _data = data;
        }

        public byte[] GetReport()
        {
            var builder = new StringBuilder();

            var dapperRows = _data.Cast<IDictionary<string, object>>().ToList();
            var firstRow = dapperRows.First();

            var header = GetHeader(firstRow);

            builder.Append(header + Environment.NewLine);
            builder.Append(GetValues(dapperRows));

            return Encoding.Default.GetBytes(builder.ToString());
        }

        private static string GetValues(IEnumerable<IDictionary<string, object>> dapperRows)
        {
            var builder = new StringBuilder();
            var rowsWithValues = dapperRows.Select(x => x.Values);

            foreach (var row in rowsWithValues)
            {
                foreach (var rowValue in row)
                    builder.Append(rowValue + ";");

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        private static string GetHeader(IDictionary<string, object> firstRow)
        {
            return firstRow.Keys.Aggregate((agg, next) => agg + ";" + next);
        }
    }
}
