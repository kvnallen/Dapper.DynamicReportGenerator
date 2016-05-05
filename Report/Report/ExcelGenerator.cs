using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace DapperDynamic.Report
{
    public class ExcelGenerator
    {
        public static ExcelPackage Generate(IEnumerable<dynamic> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var excelPackage = new ExcelPackage())
            {
                var workSheet = excelPackage.Workbook.Worksheets.Add("Report");
                var dapperRows = data.Cast<IDictionary<string, object>>().ToList();

                SetHeader(dapperRows, workSheet);

                workSheet.Cells["A1:Z1"].Style.Font.Bold = true;

                var rowsWithValues = dapperRows.Select(x => x.Values);

                var rowNumber = 2;
                foreach (var row in rowsWithValues)
                {
                    foreach (var rowValue in row.Select((r, index) => new { value = r?.ToString(), index = index + 1 }))
                        workSheet.Cells[rowNumber, rowValue.index].Value = rowValue.value;

                    rowNumber++;
                }

                return excelPackage;
            }

        }

        private static void SetHeader(IEnumerable<IDictionary<string, object>> dapperRows, ExcelWorksheet workSheet)
        {
            foreach (var key in dapperRows.First().Keys.Select((k, index) => new { value = k, index = index + 1 }))
                workSheet.Cells[1, key.index].Value = key.value;
        }
    }
}