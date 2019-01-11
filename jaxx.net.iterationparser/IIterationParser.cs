using System;
using System.Collections.Generic;
using System.Text;

namespace jaxx.net.iterationparser
{
    public interface IIterationParser
    {
        TestResultModel ParseIterationString(string result, RegExSelector selector = null, string lineDelimiter = null);
    }
}
