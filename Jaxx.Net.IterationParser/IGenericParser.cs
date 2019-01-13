using System.Collections.Generic;

namespace Jaxx.Net.IterationParser
{
    public interface IGenericParser
    {
        List<Dictionary<string, string>> ParseTestResult(string input, List<RegExSelector> selectorModel);
    }
}