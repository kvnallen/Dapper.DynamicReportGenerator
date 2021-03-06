using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DapperDynamic.Report.Tests
{
    public class CsvGeneratorTests
    {
        [Theory, Trait("ISSUE", "#2")]
        [InlineData(";")]
        [InlineData(",")]
        public void GetStringReport_WhenCalled_ReturningEmptyLines(string splitter)
        {
            List<dynamic> data = new List<dynamic>
            {
                new Dictionary<string, object>()
                {
                    ["user"] = "james",
                    ["age"] = 30,
                    ["sex"] = null
                },
                new Dictionary<string, object>
                {
                    ["user"] = null,
                    ["age"] = null,
                    ["sex"] = "male"
                },
                new Dictionary<string, object>
                {
                    ["user"] = "mr.popo",
                    ["age"] = 10000,
                    ["sex"] = "male"
                }
            };

            var expected = $"user{splitter}age{splitter}sex{Environment.NewLine}" +
                           $"james{splitter}30{splitter}{splitter}{Environment.NewLine}" +
                           $"{splitter}{splitter}male{splitter}{Environment.NewLine}" +
                           $"mr.popo{splitter}10000{splitter}male{splitter}";

            var result = CsvGenerator.GetStringReport(data, splitter);
            result.Should().Be(expected);
        }
    }
}
