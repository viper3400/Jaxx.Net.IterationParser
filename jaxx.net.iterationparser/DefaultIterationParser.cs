using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace jaxx.net.iterationparser
{
    public class DefaultIterationParser : IIterationParser
    {
        /// <summary>
        /// Parses the given input string into a list of iteration models.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="selectorModel"></param>
        /// <param name="lineDelimiter"></param>
        /// <returns></returns>
        public List<IterationModel> ParseTestResult(string input, IIterationRegExSelector selectorModel, string lineDelimiter = null)
        {
            var resultModel = new List<IterationModel>();

            var iterations = SplitIterationLines(input, lineDelimiter);
            foreach (var iterationLine in iterations)
            {               
                var iterationModel = ParseIterationLine(iterationLine, selectorModel);               
                resultModel.Add(iterationModel);
            }

            return resultModel;
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
        /// <param name="lineDelimiter">The line delimiter, default is null, then the default
        /// delimiter will be used: Environment.NewLine</param>
        /// <returns>An array of string.</returns>
        internal string[] SplitIterationLines(string input, string lineDelimiter = null)
        {
            lineDelimiter = string.IsNullOrWhiteSpace(lineDelimiter) ? Environment.NewLine : lineDelimiter;
            return Regex.Split(input, lineDelimiter);
        }
        
        private IterationModel ParseIterationLine(string iterationLine, IIterationRegExSelector selectorModel)
        {
            return new IterationModel
            {
                IterationCount = int.Parse(GetGenericString(iterationLine, selectorModel.TestIterationCountSelector)),
                IterationDate = DateTime.Parse(GetGenericString(iterationLine, selectorModel.TestIterationDateSelector)),
                IterationResult = GetGenericString(iterationLine, selectorModel.TestIterationResultSelector),
                IterationType = GetGenericString(iterationLine, selectorModel.TestIterationTypeSelector)
            };
        }
    }
}