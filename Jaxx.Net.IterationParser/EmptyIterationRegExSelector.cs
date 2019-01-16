using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class EmptyIterationRegExSelector : IIterationRegExSelector
    {
        public RegExSelector SingleLineSelector { get; set; }
        public RegExSelector TestIterationDateSelector { get; set; }
        public RegExSelector TestIterationCountSelector { get; set; }
        public RegExSelector TestIterationTypeSelector { get; set; }
        public RegExSelector TestIterationResultSelector { get; set; } 
        public RegExSelector TestIterationLineSelector { get; set; }
    }
}
