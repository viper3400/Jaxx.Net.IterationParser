using System;
using System.Collections.Generic;
using System.Text;

namespace jaxx.net.iterationparser
{
    public interface IIterationParser
    {
        TestResultModel ParseTestResult(string result);
    }
}
