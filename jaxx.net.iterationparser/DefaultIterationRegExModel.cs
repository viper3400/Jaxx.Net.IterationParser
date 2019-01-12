using System;
using System.Collections.Generic;
using System.Text;

namespace jaxx.net.iterationparser
{
    public class DefaultIterationRegExModel : IIterationRegExSelectorModel
    {
        public RegExSelector TestIterationCountSelector
        {
            get
            {
                var defaultSelectorString = @"^.*?TL( |)(\d{1,2}).*?;";
                return new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 2 };
            }

            set => throw new NotImplementedException("Use IterationRegExSelectorModel to create a custom model");
        }

        public RegExSelector TestIterationDateSelector
        {
            get
            {
                var defaultSelectorString = @"^.*?;.*?(\d{2}.\d{2}.(20\d{2}|\d{2}))";
                return new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }

            set => throw new NotImplementedException("Use IterationRegExSelectorModel to create a custom model");
        }

        public RegExSelector TestIterationResultSelector
        {
            get
            {
                var defaultSelectorString = "^.*?;.*?;(.*?)$";
                return new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }

            set => throw new NotImplementedException("Use IterationRegExSelectorModel to create a custom model");
        }

        public RegExSelector TestIterationTypeSelector
        {
            get
            {
                var defaultSelectorString = "^(.+?);";
                return new RegExSelector { Selector = defaultSelectorString, SelectedMatchGroup = 1 };
            }
            set => throw new NotImplementedException("Use IterationRegExSelectorModel to create a custom model");
        }
    }
}
