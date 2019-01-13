using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jaxx.Net.IterationParser.Tests
{
    public class JsonFileGenericRegExSelectorShould
    {
        [Fact]
        public void ReturnSingleLineSelector()
        {
            var fileSelector = new JsonFileGenericRegExSelector("GenericRegExSelectorConfiguration.json");
            Assert.Equal("\r\n",fileSelector.SingleLineSelector.Selector);
        }

        [Fact]
        public void ReturnRegExSelectors()
        {
            var fileSelector = new JsonFileGenericRegExSelector("GenericRegExSelectorConfiguration.json");
            var selectors = fileSelector.RegExSelectors;

            Assert.Equal(4, selectors.Count);
            Assert.Equal("TestIterationCountSelector", selectors[0].Name);
            Assert.Equal("^.*?TL( |)(\\d{1,2}).*?;", selectors[0].Selector);
            Assert.Equal(2, selectors[0].SelectedMatchGroup);

        }
    }
}
