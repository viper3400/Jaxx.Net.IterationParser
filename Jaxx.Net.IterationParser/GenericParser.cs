using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Jaxx.Net.IterationParser
{
    public class GenericParser : IGenericParser
    {        
        public List<Dictionary<string, string>> ParseTestResult(string input, List<RegExSelector> selectorModel)
        {
            var iterations = SplitIterationLines(
                input, 
                selectorModel.FirstOrDefault(s => s.Name == "SingleLineSelector").Selector);
            
            var resultList = new List<Dictionary<string, string>>();

            foreach (var iteration in iterations)
            {
                var iterationResult = new Dictionary<string, string>();
                foreach (var selector in selectorModel)
                {
                    iterationResult.Add(selector.Name, GetGenericString(iteration, selector));
                }                
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
    }
}
