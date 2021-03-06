﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.Net.IterationParser
{
    public class IterationModel
    {
        public DateTime IterationDate { get; set; }
        public string IterationType { get; set; }
        public int IterationCount { get; set; }
        public string IterationResult { get; set; }
        public string IterationLine { get; set; }
    }
}
