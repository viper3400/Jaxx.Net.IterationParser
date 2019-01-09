using System;
using System.Linq;
using Xunit;

namespace jaxx.net.iterationparser.tests
{
    public class DefaultIterationParserShould
    {
        [Fact]
        public void ParseResult01()
        {
            string input = "QA TL1;09.01.2018;PASSED";
            var parser = new DefaulltIterationParser();
            var resultModel = parser.ParseTestResult(input);

            Assert.Equal(DateTime.Parse("09.01.2018"), resultModel.Iterations.FirstOrDefault().IterationDate);
            Assert.Equal(1, resultModel.Iterations.FirstOrDefault().IterationCount);
            Assert.Equal("PASSED", resultModel.Iterations.FirstOrDefault().IterationResult);
            Assert.Equal("TL", resultModel.Iterations.FirstOrDefault().IterationType);

        }
    }
}
