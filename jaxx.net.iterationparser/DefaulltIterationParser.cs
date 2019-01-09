using System;
using System.Text.RegularExpressions;

namespace jaxx.net.iterationparser
{
    public class DefaulltIterationParser : IIterationParser
    {
        public TestResultModel ParseTestResult(string result)
        {
            var dateRegex01 = new Regex("\\d[2].\\d[2].20\\d[2]");
            var dateRegex02 = new Regex("\\d[2].\\d[2].\\d[2]");

            
            var resultObject = new TestResultModel();

            var splitted = result.Split(';');

        }
    }
}