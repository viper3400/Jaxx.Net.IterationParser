using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class IterationModelParser : IIterationParser, IGenericParser
    {
        public List<Dictionary<string, string>> ParseTestResult(string input, List<RegExSelector> selectorModel)
        {
            var parser = new GenericParser();
            var result = parser.ParseTestResult(input, selectorModel);
            return result;
        }

        public List<IterationModel> ParseTestResult(string input, IIterationRegExSelector selectorModel)
        {
            var selectorList = new List<RegExSelector>
            {

                selectorModel.TestIterationCountSelector,
                selectorModel.TestIterationDateSelector,
                selectorModel.TestIterationResultSelector,
                selectorModel.TestIterationTypeSelector,
                selectorModel.TestIterationLineSelector,
                selectorModel.SingleLineSelector
            };

            var iterationResults = ParseTestResult(input, selectorList);
            var resultList = new List<IterationModel>();
            foreach (var iteration in iterationResults)
            {
                var iterationModel = new IterationModel();
                int iterationCount;
                int.TryParse(iteration.FirstOrDefault(s => s.Key == "IterationCount").Value, out iterationCount);

                DateTime iterationDate = new DateTime(0);
                DateTime.TryParse(iteration.FirstOrDefault(s => s.Key == "IterationDate").Value, out iterationDate);

                iterationModel.IterationCount = iterationCount;
                iterationModel.IterationDate = iterationDate;
                iterationModel.IterationResult = iteration
                    .FirstOrDefault(s => s.Key == "IterationResult").Value;
                iterationModel.IterationType = iteration
                    .FirstOrDefault(s => s.Key == "IterationType").Value;
                iterationModel.IterationLine = iteration
                    .FirstOrDefault(s => s.Key == "IterationLine").Value;
                resultList.Add(iterationModel);
            }

            return resultList;
        }
    }
}
