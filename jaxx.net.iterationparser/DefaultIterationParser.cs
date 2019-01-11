using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace jaxx.net.iterationparser
{
    public class DefaultIterationParser : IIterationParser
    {

        /// <summary>
        /// Parse an input string and returns it's iteration model.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal IterationModel ParseIterationString (string input)
        {           
            var resultIterationDate = GetIterationDate(input);
            var resultIterationType = GetDefaultIterationType(input);
            var resultIteratioCount = GetDefaultIterationCount(input);
            var resultIterationResult = GetDefaultIterationResult(input);

            var iteration = new IterationModel();
            iteration.IterationDate = resultIterationDate;
            iteration.IterationType = resultIterationType;
            iteration.IterationCount = resultIteratioCount;
            iteration.IterationResult = resultIterationResult;

            return iteration;
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
            return result.Groups[selector.SelectedMatchGroup].Value;
        }

        /// <summary>
        /// Returns a string, parsed by the default RegExSelector for iteration type: "^(.+?);" MatchGroup 1,
        /// If you need another Selector user GetGenericString method instead.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal string GetDefaultIterationType(string input)
        {
            var defaultSelectorString = "^(.+?);";
            var selector = new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            return GetGenericString(input, selector);
        }

        /// <summary>
        /// Returns an int, parsed by the default RegExSelector for iteration count: "^.*?TL( |)(\d{1,2}).*?; MatchGroup 2,
        /// If you need another Selector user GetGenericString method instead (and cast it to int ...)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal int GetDefaultIterationCount(string input)
        {
            var defaultSelectorString = @"^.*?TL( |)(\d{1,2}).*?;";
            var selector = new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 2 };
            var result = GetGenericString(input, selector);
            return int.Parse(result);
        }

        /// <summary>
        /// Returns a string, parsed by the default RegExSelector for iteration result: "^.*?;.*?;(.*?)$" MatchGroup 1.
        /// Leading and trailing white spaces will be removed.
        /// If you need another Selector user GetGenericString method instead.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal string GetDefaultIterationResult(string input)
        {
            var defaultSelectorString = "^.*?;.*?;(.*?)$";
            var selector = new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            var iterationResult = GetGenericString(input, selector);
            return iterationResult.Trim();
        }

        /// <summary>
        /// Returns a DateTime object from a string, parsed by the given RegExSelector.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="dateSelector">The RegExSelector. Default value is null, then the default RegExSelector will
        /// be used: "^.*?;.*?(\d{2}.\d{2}.(20\d{2}|\d{2}))" - taking match group 1 </param>
        /// <returns>DateTime</returns>
        internal DateTime GetIterationDate(string input, RegExSelector dateSelector = null)
        {
            var defaultSelectorString = @"^.*?;.*?(\d{2}.\d{2}.(20\d{2}|\d{2}))";

            var selector = (dateSelector != null) ? dateSelector :
                new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };

            var regexResult = Regex.Match(input, selector.Selector);
            var resultDate = DateTime.Parse(regexResult.Groups[selector.SelectedMatchGroup].Value);
            return resultDate;
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

        public List<IterationModel> ParseTestResult(string input, IterationRegExSelectorModel selectorModel = null, string lineDelimiter = null)
        {
            var resultModel = new List<IterationModel>();

            var iterations = SplitIterationLines(input, lineDelimiter);
            foreach (var iterationLine in iterations)
            {
                IterationModel iterationModel;
                if (selectorModel == null)
                {
                    iterationModel = ParseLineWithDefaultSelectors(iterationLine);
                }
                else
                {
                    iterationModel = ParseLineWithCustomSelectors(iterationLine, selectorModel);
                }

                resultModel.Add(iterationModel);
            }

            return resultModel;
        }

        private IterationModel ParseLineWithDefaultSelectors(string iterationLine)
        {
            return new IterationModel
            {
                IterationCount = GetDefaultIterationCount(iterationLine),
                IterationDate = GetIterationDate(iterationLine),
                IterationResult = GetDefaultIterationResult(iterationLine),
                IterationType = GetDefaultIterationType(iterationLine)
            };
        }

        private IterationModel ParseLineWithCustomSelectors(string iterationLine, IterationRegExSelectorModel selectorModel)
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