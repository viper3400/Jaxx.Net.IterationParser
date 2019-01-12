using System;
using System.Collections.Generic;
using System.Text;

namespace jaxx.net.iterationparser
{
    public class IterationRegExSelectorModel : IIterationRegExSelectorModel
    {
        public RegExSelector TestIterationDateSelector { get; set; }
        public RegExSelector TestIterationCountSelector { get; set; }
        public RegExSelector TestIterationTypeSelector { get; set; }
        public RegExSelector TestIterationResultSelector { get; set; }
    }
}
