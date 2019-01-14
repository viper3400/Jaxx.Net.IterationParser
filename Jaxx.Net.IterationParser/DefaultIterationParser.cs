using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jaxx.Net.IterationParser
{
    public class DefaultIterationParser : IIterationParser, IGenericParser
    {
        /// <summary>
        /// Parses the given input string into a list of iteration models.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="selectorModel"></param>
        /// <param name="lineDelimiter"></param>
        /// <returns></returns>
        public List<IterationModel> ParseTestResult(string input, IIterationRegExSelector selectorModel)
        {
            var resultModel = new List<IterationModel>();

            var iterations = SplitIterationLines(input, selectorModel.SingleLineSelector.Selector);
            foreach (var iterationLine in iterations)
            {               
                var iterationModel = ParseIterationLine(iterationLine, selectorModel);               
                resultModel.Add(iterationModel);
            }

            return resultModel;
        }

        public List<Dictionary<string, string>> ParseTestResult(string input, List<RegExSelector> selectorModel)
        {            
            var iterationSelectorModel = new EmptyIterationRegExSelector
            {
                SingleLineSelector = selectorModel.FirstOrDefault(s => s.Name == "SingleLineSelector"),
                TestIterationCountSelector = selectorModel.FirstOrDefault(s => s.Name == "IterationCount"),
                TestIterationDateSelector = selectorModel.FirstOrDefault(s => s.Name == "IterationDate"),
                TestIterationResultSelector = selectorModel.FirstOrDefault(s => s.Name == "IterationResult"),
                TestIterationTypeSelector = selectorModel.FirstOrDefault(s => s.Name == "IterationType")
            };

            var iterationResults = ParseTestResult(input, iterationSelectorModel);
            var resultList = new List<Dictionary<string, string>>();

            foreach (var iteration in iterationResults)
            {
                var iterationResult = new Dictionary<string, string>();
                iterationResult.Add("IterationCount", iteration.IterationCount.ToString());
                iterationResult.Add("IterationDate", iteration.IterationDate.ToString());
                iterationResult.Add("IterationResult", iteration.IterationResult);
                iterationResult.Add("IterationType", iteration.IterationType);
                iterationResult.Add("IterationLine", iteration.IterationLine);
                resultList.Add(iterationResult);
            }

            return resultList;
        }

        /// <summary>
        /// Generic method to return a string, parsed by the given RegExSelector.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="selector">The RegExSelector.</param>
        /// <returns></returns>
        internal string GetGenericString(string input, RegExSelector selector)
        {
            var result = Regex.Match(input, selector.Selector);
            return result.Groups[selector.SelectedMatchGroup].Value.Trim();
        }

        /// <summary>
        /// Returns an string array, parsed and splitted by the given line delimiter.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="lineDelimiter">The line delimiter.</param>
        /// <returns>An array of string.</returns>
        internal string[] SplitIterationLines(string input, string lineDelimiter)
        {
            return Regex.Split(input, lineDelimiter);
        }
        
        private IterationModel ParseIterationLine(string iterationLine, IIterationRegExSelector selectorModel)
        {
            int iterationCount;            
            int.TryParse(GetGenericString(iterationLine, selectorModel.TestIterationCountSelector), out iterationCount);

            DateTime iterationDate = new DateTime(0);
            DateTime.TryParseExact(GetGenericString(iterationLine, selectorModel.TestIterationDateSelector),
                "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out iterationDate);
            
            return new IterationModel
            {
                IterationCount = iterationCount,
                IterationDate = iterationDate,
                IterationResult = GetGenericString(iterationLine, selectorModel.TestIterationResultSelector),
                IterationType = GetGenericString(iterationLine, selectorModel.TestIterationTypeSelector),
                IterationLine = iterationLine
            };
        }
    }
}