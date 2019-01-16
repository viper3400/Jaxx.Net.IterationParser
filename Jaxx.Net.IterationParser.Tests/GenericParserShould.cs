using System;
using System.Globalization;
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

        [Fact]
        public void SplitIterationLinesWithDefaultLineSelector()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("QA TL1; 19.08.2018; PASSED");
            inputBuilder.AppendLine("QA TL2; 20.08.2018; PASSED");
            inputBuilder.Append("QA TL1; 21.08.2018; PASSED");
            var input = inputBuilder.ToString();

            var parser = new GenericParser();
            var actual = parser.SplitIterationLines(input, new DefaultIterationRegExSelector().SingleLineSelector.Selector);
            Assert.Equal(3, actual.Count());
            Assert.Equal("QA TL1; 21.08.2018; PASSED", actual[2]);
        }

        [Fact]
        public void SplitIterationLinesWithCustomerLimiter()
        {
            var input = "QA TL1; 19.08.2018; PASSED###QA TL2; 20.08.2018; PASSED###QA TL1; 21.08.2018; PASSED";
            var parser = new GenericParser();
            var actual = parser.SplitIterationLines(input, "###");
            Assert.Equal(3, actual.Count());
            Assert.Equal("QA TL1; 21.08.2018; PASSED", actual[2]);
        }

        [Fact]
        public void ParseDateFromIterationLongFormat()
        {
            var input = "QA TL1; 19.08.2018; PASSED";

            var parser = new GenericParser();
            var actual = DateTime.ParseExact
                (parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationDateSelector),
                "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Assert.Equal(DateTime.ParseExact("19.08.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual);
        }

        [Fact]
        public void ParseDateFromIterationShortFormat()
        {
            var input = "QA TL1; 19.08.18; PASSED";

            var parser = new GenericParser();
            var actual = DateTime.ParseExact
                (parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationDateSelector),
                "dd.MM.yy", CultureInfo.InvariantCulture);

            Assert.Equal(DateTime.ParseExact("19.08.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual);
        }

        [Fact]
        public void ParseDateFromIterationLongFormatWithoutWhiteSpaces()
        {
            var input = "QA TL12;09.01.2018;FAILED";

            var parser = new GenericParser();
            var actual = DateTime.ParseExact
                (parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationDateSelector),
                "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Assert.Equal(DateTime.ParseExact("09.01.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual);
        }

        [Fact]
        public void ParseDateFromIterationCustomFormat()
        {
            var input = "QA TL1; 19.08.2018; PASSED";
            var selector = new RegExSelector { Selector = @"(\d{2}.\d{2}.(20\d{2}|\d{2}))", SelectedMatchGroup = 0 };

            var parser = new GenericParser();
            var actual = DateTime.ParseExact
                (parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationDateSelector),
                "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Assert.Equal(DateTime.ParseExact("19.08.2018", "dd.MM.yyyy", CultureInfo.InvariantCulture), actual);
        }

        [Fact]
        public void ParseGenericString()
        {
            var input = "QA TL1; 19.08.2018; PASSED";
            var selector = new RegExSelector { Selector = @"(\d{2}.\d{2}.(20\d{2}|\d{2}))", SelectedMatchGroup = 0 };

            var parser = new GenericParser();
            var actual = parser.GetGenericString(input, selector);

            Assert.Equal("19.08.2018", actual);
        }


        [Fact]
        public void ParseIterationTypeWithDefaultValue()
        {
            var input = "QA TL1; 19.08.2018; PASSED";

            var parser = new GenericParser();
            var actual = parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationTypeSelector);

            Assert.Equal("QA TL1", actual);
        }

        [Fact]
        public void ParseIterationCountWithDefaultValue()
        {
            var input = "QA TL6; 19.08.2018; PASSED";
            var parser = new GenericParser();
            var actual = int.Parse(parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationCountSelector));
            Assert.Equal(6, actual);
        }

        [Fact]
        public void ParseIterationCountWithDefaultValueAndTwoDecimals()
        {
            var input = "QA TL 16;19.08.2018; PASSED";
            var parser = new GenericParser();
            var actual = int.Parse(parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationCountSelector));
            Assert.Equal(16, actual);
        }


        [Fact]
        public void ParseIterationResultWithDefaultValue()
        {
            var input = "QA TL1; 19.08.2018;  PASSED  ";

            var parser = new GenericParser();
            var actual = parser.GetGenericString(input, new DefaultIterationRegExSelector().TestIterationResultSelector);

            Assert.Equal("PASSED", actual);
        }
    }
}
