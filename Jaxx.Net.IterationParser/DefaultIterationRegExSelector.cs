using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class DefaultIterationRegExSelector : IIterationRegExSelector
    {
        public RegExSelector TestIterationCountSelector
        {
            get
            {
                var defaultSelectorString = @"^.*?TL( |)(\d{1,2}).*?;";
                return new RegExSelector { Name = "IterationCount", Selector = defaultSelectorString, SelectedMatchGroup = 2 };
                }

            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom model");
        }

        public RegExSelector TestIterationDateSelector
        {
            get
            {
                var defaultSelectorString = @"^.*?;.*?(\d{2}.\d{2}.(20\d{2}|\d{2}))";
                return new RegExSelector { Name = "IterationDate", Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }

            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom selector.");
        }

        public RegExSelector TestIterationResultSelector
        {
            get
            {
                var defaultSelectorString = "^.*?;.*?;(.*?)$";
                return new RegExSelector { Name = "IterationResult", Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }

            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom selector.");
        }

        public RegExSelector TestIterationTypeSelector
        {
            get
            {
                var defaultSelectorString = "^(.+?);";
                return new RegExSelector { Name = "IterationType", Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }
            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom selector.");
        }

        public RegExSelector TestIterationLineSelector
        {
            get
            {
                var defaultSelectorString = "^(.+?)$";
                return new RegExSelector { Name = "IterationLine", Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }
            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom selector.");
        }

        public RegExSelector SingleLineSelector
        {
            get
            {
                var defaultSelectorString = "\r\n";
                return new RegExSelector { Name = "SingleLineSelector", Selector = defaultSelectorString };
            }
            set => throw new NotImplementedException("Use IIterationRegExSelector to create a custom selector.");
        }
    }
}
