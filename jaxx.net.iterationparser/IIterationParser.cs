using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public interface IIterationParser
    {
        List<IterationModel> ParseTestResult(string input, IIterationRegExSelector selectorModel);
    }
}
