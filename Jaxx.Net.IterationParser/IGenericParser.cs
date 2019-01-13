using System.Collections.Generic;

namespace Jaxx.Net.IterationParser
{
    public interface IGenericParser
    {
        Dictionary<string, List<GenericResultModel>> ParseTestResult(string input, List<RegExSelector> selectorModel);
    }
}