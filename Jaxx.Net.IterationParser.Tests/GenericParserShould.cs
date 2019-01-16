using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Jaxx.Net.IterationParser.Tests
{
    public class GenericParserShould
    {
        [Fact]
        public void ParseTestResultWithGenericRegExSelectorConfiguration()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("QA TL1; 19.08.2018; PASSED");
            inputBuilder.AppendLine("QA TL2; 20.08.2018; COND");
            inputBuilder.Append("QA TL1; 21.08.2018; PASSED");
            var input = inputBuilder.ToString();

            var parser = new GenericParser();
            var actual = parser.ParseTestResult(input, new JsonFileGenericRegExSelector("GenericRegExSelectorConfiguration.json").RegExSelectors);

            Assert.Equal(3, actual.Count());

            Assert.Equal("20.08.2018", actual[1]["IterationDate"]);
            Assert.Equal("2", actual[1]["IterationCount"]);
            Assert.Equal("COND", actual[1]["IterationResult"]);
            Assert.Equal("QA TL2", actual[1]["IterationType"]);
            Assert.Equal("QA TL2; 20.08.2018; COND", actual[1]["IterationLine"]);
        }
    }
}
