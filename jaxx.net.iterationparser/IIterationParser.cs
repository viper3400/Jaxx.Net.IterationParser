using System;
using System.Collections.Generic;
using System.Text;

namespace jaxx.net.iterationparser
{
    public interface IIterationParser
    {
        List<IterationModel> ParseTestResult(string input, IIterationRegExSelectorModel selectorModel = null, string lineDelimiter = null);
    }
}
