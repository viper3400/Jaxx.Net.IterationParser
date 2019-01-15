using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace Jaxx.Net.IterationParser.Tests
{
    public class IterationModelParserShould
    {
        [Fact]
        public void ParseTestResultWithDefaultSelectors()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("QA TL1; 19.08.2018; PASSED");
            inputBuilder.AppendLine("QA TL2; 20.08.2018; COND");
            inputBuilder.Append("QA TL1; 21.08.2018; PASSED");
            var input = inputBuilder.ToString();

            var parser = new IterationModelParser();
            var actual = parser.ParseTestResult(input, new DefaultIterationRegExSelector());

            Assert.Equal(3, actual.Count());

            Assert.Equal(DateTime.ParseExact("20.08.2018","dd.MM.yyyy", CultureInfo.InvariantCulture), actual[1].IterationDate);
            Assert.Equal(2, actual[1].IterationCount);
            Assert.Equal("COND", actual[1].IterationResult);
            Assert.Equal("QA TL2", actual[1].IterationType);
            //Assert.Equal("QA TL2; 20.08.2018; COND", actual[1].IterationLine);
        }

        [Fact]
        public void ParseTestResultWithGenericRegExSelectorConfiguration()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("QA TL1; 19.08.2018; PASSED");
            inputBuilder.AppendLine("QA TL2; 20.08.2018; COND");
            inputBuilder.Append("QA TL1; 21.08.2018; PASSED");
            var input = inputBuilder.ToString();

            var parser = new IterationModelParser();
            var actual = parser.ParseTestResult(input, new JsonFileGenericRegExSelector("GenericRegExSelectorConfiguration.json").RegExSelectors);

            Assert.Equal(3, actual.Count());

            Assert.Equal("20.08.2018", actual[1]["IterationDate"]);
            Assert.Equal("2", actual[1]["IterationCount"]);
            Assert.Equal("COND", actual[1]["IterationResult"]);
            Assert.Equal("QA TL2", actual[1]["IterationType"]);
            //Assert.Equal("QA TL2; 20.08.2018; COND", actual[1]["IterationLine"]);
        }

        [Fact]
        public void ParseTestResultAndDealWithUnparsableLines()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("QA Review;19..2018");
            inputBuilder.AppendLine(" ;20.08.2018; COND");
            inputBuilder.Append("TOTALY WRONG CONTENT");
            var input = inputBuilder.ToString();

            var parser = new IterationModelParser();
            var actual = parser.ParseTestResult(input, new DefaultIterationRegExSelector());

            Assert.Equal(3, actual.Count());

            Assert.Equal(DateTime.ParseExact("01.01.0001", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual[0].IterationDate);
            Assert.Equal(0, actual[0].IterationCount);
            Assert.Equal("", actual[0].IterationResult);
            Assert.Equal("QA Review", actual[0].IterationType);
            //Assert.Equal("QA Review;19..2018", actual[0].IterationLine);

            Assert.Equal(DateTime.ParseExact("20.08.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual[1].IterationDate);
            Assert.Equal("", actual[1].IterationType);
            Assert.Equal("COND", actual[1].IterationResult);

            //Assert.Equal("TOTALY WRONG CONTENT", actual[2].IterationLine);
            Assert.Equal(DateTime.ParseExact("01.01.0001", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual[2].IterationDate);
            Assert.Equal("", actual[2].IterationResult);
            Assert.Equal(0, actual[2].IterationCount);
            Assert.Equal("", actual[2].IterationType);
        }
    }
}
